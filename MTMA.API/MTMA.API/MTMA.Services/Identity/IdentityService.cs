namespace MTMA.Services.Identity
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using MTMA.Data.Models.Identity;
    using MTMA.Services.JwtGenerator;
    using MTMA.Services.Mapping;
    using MTMA.Services.ServiceModels;
    using MTMA.Services.ServiceModels.Services.Identity;

    public class IdentityService : IIdentityService
    {
        private readonly ILogger<IdentityService> _logger;
        private readonly UserManager<MTMAUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtGeneratorService _jwtGeneratorService;
        public IdentityService(
            ILogger<IdentityService> logger,
            UserManager<MTMAUser> userManager,
            IJwtGeneratorService jwtGeneratorService)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._jwtGeneratorService = jwtGeneratorService;
            this._mapper = AutoMapperConfig.MapperInstance;
        }

        public async Task<IdentityResultServiceModel> Register(RegisterUserServiceModel serviceModel)
        {
            try
            {
                var user = this._mapper.Map<RegisterUserServiceModel, MTMAUser>(serviceModel);

                var identityResult = await this._userManager.CreateAsync(
                        user,
                        serviceModel.Password);

                return this._mapper.Map<IdentityResult, IdentityResultServiceModel>(identityResult);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Registration failed while processing {MethodName}.", nameof(Register));
                return new IdentityResultServiceModel
                {
                    Errors = new List<string>
                    {
                        $"Failed to register the user '{serviceModel.UserName}'."
                    }
                };
            }
        }

        public async Task<(IdentityResultServiceModel, string)> Login(LoginUserServiceModel serviceModel)
        {
            try
            {
                var identityResult = new IdentityResultServiceModel
                {
                    Succeeded = true,
                    Errors = new List<string>
                    {
                        "Invalid email or password"
                    }
                };

                // Validate user
                var user = await this._userManager.FindByEmailAsync(serviceModel.Email);
                if (user is null)
                {
                    identityResult.Succeeded = false;
                    return (identityResult,default!);
                }

                // Validate password
                var passwordValid = await this._userManager.CheckPasswordAsync(
                    user,
                    serviceModel.Password);
                if (passwordValid is false)
                {
                    identityResult.Succeeded = false;
                    return (identityResult, default!);
                }

                var token = await this._jwtGeneratorService.GenerateToken(user);
                
                return (identityResult, token);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Login failed while processing {MethodName}.", nameof(Login));
                return (new IdentityResultServiceModel
                {
                    Errors = new List<string>
                    {
                        $"Failed to login."
                    }
                }, default!);
            }
        }
    }
}

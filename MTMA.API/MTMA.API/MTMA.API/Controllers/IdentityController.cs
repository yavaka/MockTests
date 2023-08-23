namespace MTMA.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MTMA.Services.Identity;
    using MTMA.Services.ServiceModels;
    using MTMA.Services.ServiceModels.Services.Identity;

    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
            => this._identityService = identityService;

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register(RegisterUserServiceModel serviceModel)
        {
            var identityResult = await this._identityService.Register(serviceModel);

            return identityResult.Succeeded
                ? this.Ok()
                : this.BadRequest(identityResult.Errors);
        }

        [HttpGet]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login([FromQuery] LoginUserServiceModel serviceModel)
        {
            var (identityResult, token) = await this._identityService.Login(serviceModel);

            return identityResult.Succeeded
                ? this.Ok(token)
                : this.BadRequest(identityResult.Errors);
        }
    }
}

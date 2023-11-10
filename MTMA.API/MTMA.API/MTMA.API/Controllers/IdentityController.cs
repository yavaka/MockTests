namespace MTMA.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MTMA.Services.Identity;
    using MTMA.Services.ServiceModels;

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
                : this.BadRequest(identityResult);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginUserServiceModel serviceModel)
        {
            var (identityResult, token) = await this._identityService.Login(serviceModel);

            return identityResult.Succeeded
                ? this.Ok(new { token = token })
                : this.BadRequest(identityResult);
        }

        [HttpPost]
        [Authorize]
        [Route(nameof(ChangePassword))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordServiceModel serviceModel)
        {
            var identityResult = await this._identityService.ChangePassword(serviceModel);

            return identityResult.Succeeded
                ? this.Ok()
                : this.BadRequest(identityResult);
        }
    }
}

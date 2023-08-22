namespace MTMA.API.Controllers
{
    using FluentValidation;
    using FluentValidation.AspNetCore;
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
                : this.BadRequest(identityResult.Errors);
        }
    }
}

namespace MTMA.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                this._logger.LogError(e, $"This is Slack Logger test: {nameof(this.Index)}");
            }

            return this.Ok();
        }
    }
}

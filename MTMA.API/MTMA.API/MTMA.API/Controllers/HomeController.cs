using Microsoft.AspNetCore.Mvc;

namespace MTMA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                this._logger.LogError(e, $"{nameof(this.Index)}");
            }

            this._logger.LogWarning("This is logged Warning msg");

            return this.Ok();
        }
    }
}

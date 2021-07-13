using Microsoft.AspNetCore.Mvc;

namespace WebApiGames_Demo.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/test")]
    [ApiController]
    public class TestV2Controller : ControllerBase
    {
        [HttpGet("v2")]
        public IActionResult Get()
        {
            return Content("<html><body><h2>TestV2Controller - V 2.0 </h2></body></html>");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace WebApiGames_Demo.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/test")]
    [ApiController]
    public class TestV1Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get ()
        {
            return Content("<html><body><h2>TestV1Controller - V 1.0 </h2></body></html>");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ClassLibrary1
{
    [ApiController]
    [Route("Api/v2/")]
    public class Class1 : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ciao");
        }
    }
}

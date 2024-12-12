
using BusinessLogic.Options;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BusinessLogic.Controller
{
    [ApiController]
    [Route("Api/v1/")]
    public class BusinessController : ControllerBase
    {
        private BusinessServices _ctx;
        private IOptions<BusinessOptions> _conf;
        private HttpClient _cli;
        public BusinessController(BusinessServices ctx, IOptions<BusinessOptions> conf)
        {
            _ctx = ctx;
            _conf = conf;
        }

        [HttpGet("GetData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetData()
        {
            var t = _conf.Value;
            var data = await _ctx.GetCategories();
            return Ok(data);
        }

        [HttpGet]
        public int GetNumber()
        {
            return _ctx.GetNumber() ;
        }

        [HttpGet("ApiData")]
        public async Task<string> GetApiData() 
        {
            return await _ctx.GetNumberFromApi(_cli);
        }
    }
}

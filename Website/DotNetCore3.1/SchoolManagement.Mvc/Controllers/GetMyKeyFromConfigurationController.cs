using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Mvc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetMyKeyFromConfigurationController : ControllerBase
    {
        private IConfiguration _configuration;
        public GetMyKeyFromConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public string Get()
        {
            return _configuration["MyKey"];
        }
    }
}

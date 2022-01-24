using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DotNetCore3._1WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetMyKeyFromConfigurationController : Controller
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

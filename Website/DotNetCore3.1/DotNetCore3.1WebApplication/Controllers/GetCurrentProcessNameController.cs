using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore3._1WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetCurrentProcessNameController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        }
    }
}

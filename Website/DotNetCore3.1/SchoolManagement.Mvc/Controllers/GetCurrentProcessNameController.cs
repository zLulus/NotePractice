using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Mvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetCurrentProcessNameController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        }
    }
}

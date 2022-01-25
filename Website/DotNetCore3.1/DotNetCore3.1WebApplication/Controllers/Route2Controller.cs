using Microsoft.AspNetCore.Mvc;

namespace DotNetCore3._1WebApplication.Controllers
{
    //属性路由支持多层
    [Route("Route2")]
    public class Route2Controller : Controller
    {
        [Route("")]
        [Route("Index")]
        [Route("TestRoute")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("GetNumber/{number?}")]
        public IActionResult GetNumber(int? number)
        {
            return View(number);
        }
    }
}

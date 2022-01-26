using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Mvc.Controllers
{
    //属性路由
    public class RouteController : Controller
    {
        [Route("Route")]
        [Route("Route/Index")]
        [Route("Route/TestRoute")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Route/GetNumber/{number?}")]
        public IActionResult GetNumber(int? number)
        {
            return View(number);
        }
    }
}

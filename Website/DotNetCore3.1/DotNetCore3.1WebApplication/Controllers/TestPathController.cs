using Microsoft.AspNetCore.Mvc;

namespace DotNetCore3._1WebApplication.Controllers
{
    public class TestPathController : Controller
    {
        public IActionResult Index()
        {
            return View("Test");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Mvc.Controllers
{
    public class TestPathController : Controller
    {
        public IActionResult Index()
        {
            return View("Test");
        }
    }
}

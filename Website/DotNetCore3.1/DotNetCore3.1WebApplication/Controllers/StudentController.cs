using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore3._1WebApplication.Controllers
{
    public class StudentController : Controller
    {
        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Edit
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }
    }
}

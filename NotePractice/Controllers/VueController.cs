using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class VueController : Controller
    {
        // GET: Vue
        public ActionResult HelloWorld()
        {
            return View();
        }

        public ActionResult Bind()
        {
            return View();
        }
        public ActionResult See()
        {
            return View();
        }

        public ActionResult For()
        {
            return View();
        }
    }
}
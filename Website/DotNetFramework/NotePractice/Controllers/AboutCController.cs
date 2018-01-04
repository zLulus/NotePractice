using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class AboutCController : Controller
    {
        // GET: AboutC
        public ActionResult ReturnObjectFromC()
        {
            return View();
        }

        public ActionResult ReturnObjectFromCAjax(string A,string B)
        {
            return Json(new
            {
                A = A + "_ok",
                B = B + "_ok",
                Success=true
            });
        }
    }
}
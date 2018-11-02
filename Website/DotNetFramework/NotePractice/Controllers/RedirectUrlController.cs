using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class RedirectUrlController : Controller
    {
        // GET: RedirectUrl
        public ActionResult RedirectByForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RedirectByForm(string url)
        {
            return Redirect(url);
        }

        public ActionResult RedirectByAjax()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RedirectByAjax(string url)
        {
            return Json(url);
        }
    }
}
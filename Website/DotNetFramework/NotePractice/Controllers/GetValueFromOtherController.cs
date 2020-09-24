using NotePractice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class GetValueFromOtherController : Controller
    {
        // GET: GetValueFromOther
        public ActionResult Index()
        {
            var v = TempData["GetValueFromOther"] as GetValueFromOtherViewModel;
            return Json(v.Value,JsonRequestBehavior.AllowGet);
        }
    }
}
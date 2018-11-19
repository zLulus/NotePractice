using NotePractice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class GoToOtherController : Controller
    {
        // GET: GoToOther
        public ActionResult Index()
        {
            var vm = new GetValueFromOtherViewModel
            {
                Value="123"
            };
            TempData["GetValueFromOther"] = vm;
            return RedirectToAction("Index", "GetValueFromOther");
        }
    }
}
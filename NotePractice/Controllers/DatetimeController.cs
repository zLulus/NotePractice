using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class DatetimeController : Controller
    {
        // GET: Datetime
        public ActionResult Daterangepicker()
        {
            return View();
        }

        public ActionResult Datetimepicker()
        {
            return View();
        }
    }
}
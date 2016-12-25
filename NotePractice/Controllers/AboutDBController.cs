using BLL.Models.AboutDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace NotePractice.Controllers
{
    public class AboutDBController : Controller
    {
        // GET: AboutDB
        public ActionResult Bit()
        {
            List<BitModel> list = new AboutDBManager().GetBitModel();
            return View(list);
        }

        public ActionResult ExcuteSQL()
        {
            new AboutDBManager().InsertBitModelByTraditionalWay();
            return View();
        }
    }
}
using BLL.Models.AboutDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class AboutDBController : Controller
    {
        // GET: AboutDB
        public ActionResult Bit()
        {
            List<BitModel> list = new BLL.AboutDBManager().GetBitModel();
            return View(list);
        }
    }
}
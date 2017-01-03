using BLL.Models.AboutDB;
using System.Collections.Generic;
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

        public ActionResult Area()
        {
            ViewBag.Province = new AboutDBManager().GetArea(0);
            return View();
        }

        public ActionResult GetArea(int ParentID)
        {
            var regions = new AboutDBManager().GetArea(ParentID);
            return Json(regions, JsonRequestBehavior.AllowGet);
        }
    }
}
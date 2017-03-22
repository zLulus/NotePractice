using BLL.Models.Js;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class JsController : Controller
    {
        public ActionResult Refresh()
        {
            return View();
        }

        public ActionResult DOMAndJQuery()
        {
            return View();
        }

        public ActionResult GetValue()
        {
            return View();
        }

        public ActionResult DropDownList()
        {
            List<DictionaryModel> dic = new List<DictionaryModel>();
            dic.Add(new DictionaryModel() { PKID = 1, DicKey = "1Using", DicValue = "使用中" });
            dic.Add(new DictionaryModel() { PKID = 2, DicKey = "2", DicValue = "未投入使用" });
            dic.Add(new DictionaryModel() { PKID = 3, DicKey = "3", DicValue = "已废弃" });
            ViewBag.UseStates = dic;
            ViewBag.UseState = 1;
            return View();
        }

        public ActionResult Ajax()
        {
            return View();
        }
        public ActionResult GetWithoutParameter()
        {
            return Json("",JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetWithParameter(string FirstName,string LastName)
        {
            return Json("",JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PostWithParameters(int number1,int number2)
        {
            return Json(number1+ number2);
        }

        public ActionResult DropDownList2()
        {
            return View();
        }

        public ActionResult TabSwitch()
        {
            return View();
        }
    }
}
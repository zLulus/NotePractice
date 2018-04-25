using Model.WechatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class AccountForMiniProgramController : Controller
    {
        [HttpGet]
        public ActionResult GetData()
        {
            return Json(new { time=DateTime.Now,data=123 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult WechatGetSessionKey(WechatGetSessionKeyInput input)
        {
            return Json(new { result = input.id });
        }
    }
}
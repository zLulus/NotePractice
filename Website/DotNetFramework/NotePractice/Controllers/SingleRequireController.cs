using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotePractice.Interceptor;

namespace NotePractice.Controllers
{
    public class SingleRequireController : Controller
    {
        [SingleRequire]
        public ActionResult TestSingleRequire()
        {
            return Json("请求成功！",JsonRequestBehavior.AllowGet);
        }
    }
}
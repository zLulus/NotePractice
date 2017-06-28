using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class EncryptionController : Controller
    {
        public ActionResult Base64View()
        {
            return View();
        }

        public ActionResult Md5View()
        {
            return View();
        }

        public ActionResult Sha1View()
        {
            return View();
        }
    }
}
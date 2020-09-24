using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Controllers
{
    public class ResponseCacheController : Controller
    {
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 60,Location =ResponseCacheLocation.Any)]
        public IActionResult TestResponseCache()
        {
            ViewBag.time = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");

            return View();
        }
    }
}
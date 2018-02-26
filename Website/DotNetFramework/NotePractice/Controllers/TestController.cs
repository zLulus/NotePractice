using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://wx.ddxc.org/weixin/WeiXinRedPackInterface");
            var reuslt = await response.Content.ReadAsStringAsync();
            return Json(reuslt, JsonRequestBehavior.AllowGet);
        }
    }
}
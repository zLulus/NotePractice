using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class ClientInfoController : Controller
    {
        // GET: ClientInfo
        public ActionResult GetClientInfo()
        {
            string strHostName = System.Net.Dns.GetHostName();
            var entry = System.Net.Dns.GetHostEntry(strHostName);
            //clientIPAddress是一个数组，可能有多个数据
            var clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName);
            string clientip = clientIPAddress.GetValue(0).ToString();
            return Json(clientip, JsonRequestBehavior.AllowGet);
        }
    }
}
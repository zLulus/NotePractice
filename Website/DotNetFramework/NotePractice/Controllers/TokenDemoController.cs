using Model.TokenDemo;
using Model.TokenDemo.LogInResponses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class TokenDemoController : Controller
    {
        string baseUrl = "http://localhost:1107/";
        // GET: TokenDemo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> LogIn(LogInRequest request)
        {
            HttpClient client = new HttpClient();
            string str = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(str, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(baseUrl + "/account/Authenticate", content);
            var r = result.Content.ReadAsStringAsync().Result;
            LogInResponse response = JsonConvert.DeserializeObject<LogInResponse>(r);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetUserInfo(GetUserInfoRequest request)
        {
            try
            {
                HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);
                string str = JsonConvert.SerializeObject("");
                StringContent content = new StringContent(str, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(baseUrl + "/api/services/app/mobileTerminal/GetUserCompanyWithStatisticalAsync", content);
                var r = result.Content.ReadAsStringAsync().Result;
                return Json(r, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return null;
            }
           
        }

        public async Task<ActionResult> LogOut(LogInRequest request)
        {
            HttpClient client = new HttpClient();
            string str = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(str, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(baseUrl + "/account/LogoutForApp", content);
            var r = result.Content.ReadAsStringAsync().Result;
            return Json(r, JsonRequestBehavior.AllowGet);
        }
    }
}
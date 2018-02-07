using Newtonsoft.Json;
using Senparc.Weixin;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class WeiXinController : Controller
    {
        private string authorizeUrl = "";
        private string appId = "";
        private string appSecret = "";
        
        public async Task<ActionResult> GetWechatUserInfoInterface(string returnUrl)
        {
            try
            {
                var state = "zl-" + DateTime.Now.Millisecond; //随机数，用于识别请求可靠性
                Session["zl-weixin-sate"] = state; //储存随机数到Session
                var userLogUrl = authorizeUrl + "/weixin/GetWechatUserInfoCallBackInterface?returnUrl=" + returnUrl.UrlEncode();
                //微信授权网址
                //先调微信的接口，再返回带的自己的接口
                var urlUserInfo = OAuthApi.GetAuthorizeUrl(appId, userLogUrl, state, OAuthScope.snsapi_userinfo);
                return Redirect(urlUserInfo);
            }
            catch (Exception ex)
            {
                return Json("发生了错误！");
            }
        }

        /// <summary>
        ///     用户验证回掉函数
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetWechatUserInfoCallBackInterface(string code, string state, string returnUrl)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Json("发生了错误！");
            }
            if (state != Session["zl-weixin-sate"] as string)
            {
                return Json("发生了错误！");
            }
            try
            {
                var token = await OAuthApi.GetAccessTokenAsync(appId, appSecret, code);
                if (token.errcode == ReturnCode.请求成功)
                {
                    //此处获得用户信息
                    var oAuthUserInfo = await OAuthApi.GetUserInfoAsync(token.access_token, token.openid);
                    return Json(JsonConvert.SerializeObject(oAuthUserInfo), JsonRequestBehavior.AllowGet);
                }
                return RedirectToAction("Error", "Layout");
            }
            catch (Exception ex)
            {
                return Json("发生了错误！");
            }
        }
    }
}
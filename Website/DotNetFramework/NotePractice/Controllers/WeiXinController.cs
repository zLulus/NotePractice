using Model.WechatModel;
using Newtonsoft.Json;
using QRCoder;
using Senparc.Weixin;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        private Dictionary<string,User> bindWechatDictionary { get; set; }

        static WeiXinController()
        {

        }

        public WeiXinController()
        {
            bindWechatDictionary = new Dictionary<string, User>();
        }

        #region 查询微信用户信息
        public async Task<ActionResult> GetWechatUserInfoInterface(string returnUrl)
        {
            var state = "zl-" + DateTime.Now.Millisecond; //随机数，用于识别请求可靠性
            Session["zl-weixin-sate"] = state; //储存随机数到Session
            var userLogUrl = authorizeUrl + "/weixin/GetWechatUserInfoCallBackInterface?returnUrl=" + returnUrl.UrlEncode();
            //微信授权网址
            //先调微信的接口，再返回带的自己的接口
            var urlUserInfo = OAuthApi.GetAuthorizeUrl(appId, userLogUrl, state, OAuthScope.snsapi_userinfo);
            return Redirect(urlUserInfo);
        }

        /// <summary>
        ///     用户验证回掉函数
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetWechatUserInfoCallBackInterface(string code, string state, string returnUrl)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Json("您拒绝了授权！");
            }
            if (state != Session["zl-weixin-sate"] as string)
            {
                return Json("验证失败！请从正规途径进入！");
            }
            try
            {
                var token = await OAuthApi.GetAccessTokenAsync(appId, appSecret, code);
                if (token.errcode == ReturnCode.请求成功)
                {
                    //此处获得微信的用户信息
                    var oAuthUserInfo = await OAuthApi.GetUserInfoAsync(token.access_token, token.openid);
                    return Json(JsonConvert.SerializeObject(oAuthUserInfo), JsonRequestBehavior.AllowGet);
                }
                return Json("请求失败！");
            }
            catch (Exception ex)
            {
                return Json("Token授权失败！");
            }
        }

        #endregion

        #region 绑定系统用户和微信用户信息
        public ActionResult BindWeChat()
        {
            return View();
        }

        public ActionResult GetBindWeChatImg()
        {
            //查询在系统里面的用户信息
            var user = new User()
            {
                Id = 1,
                UserName = "Lulu"
            };
            string authorizeCode = Guid.NewGuid().ToString();
            //将用户信息和唯一标志绑定
            bindWechatDictionary.Add(authorizeCode, user);

            //生成二维码
            string url = $"?authorizeCode={authorizeCode}";
            QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrcode.GetGraphic(5, Color.Black, Color.White, null, 15, 6, false);
            MemoryStream ms = new MemoryStream();
            qrCodeImage.Save(ms, ImageFormat.Jpeg);
            return File(ms.ToArray(), "image/jpeg");
        }

        public async Task<ActionResult> GetWechatUserInfoInterfaceForBindWeChat(string authorizeCode)
        {
            var state = "zl-" + DateTime.Now.Millisecond; //随机数，用于识别请求可靠性
            Session["zl-weixin-sate"] = state; //储存随机数到Session
            var userLogUrl = authorizeUrl + "/weixin/BindWeChatInterface?authorizeCode=" + authorizeCode.UrlEncode();
            //微信授权网址
            //先调微信的接口，再返回带的自己的接口
            var urlUserInfo = OAuthApi.GetAuthorizeUrl(appId, userLogUrl, state, OAuthScope.snsapi_userinfo);
            return Redirect(urlUserInfo);
        }

        /// <summary>
        ///     用户验证回掉函数
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> BindWeChatInterface(string code, string state, string authorizeCode)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Json("您拒绝了授权！");
            }
            if (state != Session["zl-weixin-sate"] as string)
            {
                return Json("验证失败！请从正规途径进入！");
            }
            try
            {
                var token = await OAuthApi.GetAccessTokenAsync(appId, appSecret, code);
                if (token.errcode == ReturnCode.请求成功)
                {
                    //此处获得微信的用户信息
                    var oAuthUserInfo = await OAuthApi.GetUserInfoAsync(token.access_token, token.openid);
                    var user = bindWechatDictionary.FirstOrDefault(x => x.Key == authorizeCode).Value;
                    if (user != null)
                    {
                        //调用接口，绑定系统用户和微信用户信息
                        string data = $"微信用户信息：{JsonConvert.SerializeObject(oAuthUserInfo)}\n系统用户信息：{JsonConvert.SerializeObject(user)}";
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("当前授权码没有查询到对应的用户信息", JsonRequestBehavior.AllowGet);
                    }
                    
                }
                return Json("请求失败！");
            }
            catch (Exception ex)
            {
                return Json("Token授权失败！");
            }
        }
        #endregion
    }
}
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WechatDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //微信查询用户信息
            string appId = "";
            string appSecret = "";
            string openId = "";
            //检查是否已经注册
            if (!AccessTokenContainer.CheckRegistered(appId))
            {
                //如果没有注册则进行注册，2小时token过期
                AccessTokenContainer.Register(appId, appSecret);
            }
            string token = AccessTokenContainer.GetAccessToken(appId);
            //如果appId,appSecret和openId不配套，报错：微信请求发生错误！错误代码：40003，说明：invalid openid hint: [2BZbQA00621527]
            var info = UserApi.Info(token, openId);

            info = UserApi.Info(appId, openId);
        }
    }
}

using Newtonsoft.Json;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WechatDemo.Models.GetAllUsers;

namespace WechatDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //查询目标用户的信息
            //GetTargetUserInfo();

            Task.Run(async () =>
            {
                //查询所有用户
                await GetAllUsers();
            });

            Console.ReadLine();
        }
        private static async Task GetAllUsers()
        {
            using (HttpClient client = new HttpClient())
            {
                var appId = "";
                var appSecret = "";
                //起始位置，为空则从头开始查询
                var next_openid = "";
                //注册一次即可
                AccessTokenContainer.Register(appId, appSecret);
                var token = AccessTokenContainer.GetAccessToken(appId);
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}", token);
                if (!string.IsNullOrEmpty(next_openid))
                {
                    url += "&next_openid=" + next_openid;
                }
                var response = await client.GetAsync(url);
                var resultStr = await response.Content.ReadAsStringAsync();
                GetAllUsersOutput result = JsonConvert.DeserializeObject<GetAllUsersOutput>(resultStr);
            }
        }

        private static void GetTargetUserInfo()
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.SpoofIpAddress
{
    public class SpoofIpAddressDemo
    {
        //http://voidm.com/2018/04/04/c-91porn-video-parse/

        // 创建GET方式的HTTP请求
        public static string get(string url, int timeout = 5000)
        {
            string retString;
            Console.WriteLine("Http.Get : " + url);
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                // 这里设置随机IP
                //"X-FORWARDED-FOR" 是代理服务器通过 HTTP Headers 提供的客户端IP。代理服务器可以伪造任何IP。
                //要防止伪造，不要读这个IP即可（同时告诉用户不要用HTTP 代理）。
                request.Headers.Add("X-Forwarded-For", getRandomIp());
                request.Headers.Add("CLIENT_IP", getRandomIp());
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                request.UserAgent = null;
                request.Timeout = timeout;

                request.CookieContainer = new CookieContainer();
               
                var cookies = new CookieCollection();
                // 这个cookie的作用是资源语言设置为中文
                var cookie = new Cookie
                {
                    Name = "language",
                    Value = "cn_CN",
                    Path = "/",
                    Domain = "93.91p09.space"
                };
                cookies.Add(cookie);
                request.CookieContainer.Add(cookies);
                

                var response = (HttpWebResponse)request.GetResponse();
                var myResponseStream = response.GetResponseStream();
                var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch
            {
                return null;
            }
            return retString;
        }

        // 获取随机IP地址
        private static string getRandomIp()
        {
            int[][] range = {
                new[]{607649792, 608174079}, //36.56.0.0-36.63.255.255
                new[]{1038614528, 1039007743}, //61.232.0.0-61.237.255.255
                new[]{1783627776, 1784676351}, //106.80.0.0-106.95.255.255
                new[]{2035023872, 2035154943}, //121.76.0.0-121.77.255.255
                new[]{2078801920, 2079064063}, //123.232.0.0-123.235.255.255
                new[]{-1950089216, -1948778497}, //139.196.0.0-139.215.255.255
                new[]{-1425539072, -1425014785}, //171.8.0.0-171.15.255.255
                new[]{-1236271104, -1235419137}, //182.80.0.0-182.92.255.255
                new[]{-770113536, -768606209}, //210.25.0.0-210.47.255.255
                new[]{-569376768, -564133889}, //222.16.0.0-222.95.255.255
            };

            var rdint = new Random();
            var index = rdint.Next(10);
            var ip = num2ip(range[index][0] + new Random().Next(range[index][1] - range[index][0]));
            return ip;
        }

        // 将十进制转换成ip地址
        private static string num2ip(int ip)
        {
            var b = new int[4];
            //位移然后与255 做高低位转换
            b[0] = (ip >> 24) & 0xff;
            b[1] = (ip >> 16) & 0xff;
            b[2] = (ip >> 8) & 0xff;
            b[3] = ip & 0xff;
            return b[0] + "." + b[1] + "." + b[2] + "." + b[3];
        }
    }
}

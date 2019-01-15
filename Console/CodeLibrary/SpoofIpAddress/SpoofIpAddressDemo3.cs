using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.SpoofIpAddress
{
    public class SpoofIpAddressDemo3
    {
        /// <summary>
        /// 在请求时设置代理
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="url"></param>
        public static void SpoofIpAddressBySetProxyWhileRequest(string ip, int port,string url)
        {
            //https://stackoverflow.com/questions/29856543/httpclient-and-using-proxy-constantly-getting-407
            
            // First create a proxy object

            var proxy = new WebProxy()
            {
                Address = new Uri($"http://{ip}:{port}"),
                UseDefaultCredentials = false,

                //如果该代理需要账号密码，设置账号密码
                // *** These creds are given to the proxy server, not the web server ***
                //Credentials = new NetworkCredential(
                //    userName: proxyUserName,
                //    password: proxyPassword);
            };

            // Now create a client handler which uses that proxy

            var httpClientHandler = new HttpClientHandler()
            {
                Proxy = proxy,
                UseProxy = true
            };

            // Omit this part if you don't need to authenticate with the web server:
            //if (needServerAuthentication)
            //{
            //    httpClientHandler.PreAuthenticate = true;
            //    httpClientHandler.UseDefaultCredentials = false;

            //    // *** These creds are given to the web server, not the proxy server ***
            //    httpClientHandler.Credentials = new NetworkCredential(
            //        userName: serverUserName,
            //        password: serverPassword);
            //}

            // Finally, create the HTTP client object

            var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
            var response = client.GetAsync(url).Result;
            var str = response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// 全局设置代理
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public static void SpoofIpAddressBySetProxyForSystem(string ip,int port)
        {
            SetProxy($"{ip}:{port}");
            //在这里发送请求
            UnSetProxy();
        }

        /// <summary>
        ///  验证代理IP地址是否可用
        /// </summary>
        /// <param name="callback">是否可用</param>
        /// <param name="ip">地址</param>
        /// <param name="port">端口</param>
        public static void ChecKedForIP(Action<bool> callback, string ip, int port,string url= "http://www.baidu.com", int timeout = 0)
        {
            try
            {
                string Reshtml = string.Empty;//请求返回的结果
                WebProxy webproxy = new WebProxy(ip, port);  //实例代理设置
                HttpWebRequest Httpweq = (HttpWebRequest)WebRequest.Create(url);
                Httpweq.Proxy = webproxy;//模拟设置代理请求
                HttpWebResponse HttpRespon = (HttpWebResponse)Httpweq.GetResponse();
                Httpweq.Timeout = timeout != 0 ? timeout : 10000; //默认10秒，如果设置值，就用设置的值
                Encoding encoding = Encoding.GetEncoding("gb2312");//设置编码格式为汉字，防止英文系统，无法解码
                using (StreamReader reader = new StreamReader(HttpRespon.GetResponseStream(), encoding))
                {
                    Reshtml = reader.ReadToEnd().Trim();
                    if (!string.IsNullOrEmpty(Reshtml))
                    {
                        callback(true);
                    }
                    else
                    {
                        callback(false);
                    }
                }

            }
            catch (Exception ex)
            {

                callback(false);
            }
            finally { }

        }


        [DllImport(@"wininet", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "InternetSetOption", CallingConvention = CallingConvention.StdCall)]
        public static extern bool InternetSetOption
               (
               int hInternet,
               int dmOption,
               IntPtr lpBuffer,
               int dwBufferLength
               );

        /// <summary>
        /// 设置代理
        /// </summary>
        /// <param name="ip_port">IP地址和端口</param>
        public static void SetProxy(string ip_port)
        {
            //打开注册表
            RegistryKey regKey = Registry.CurrentUser;
            string SubKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";
            RegistryKey optionKey = regKey.OpenSubKey(SubKeyPath, true);
            //更改健值，设置代理，
            optionKey.SetValue("ProxyEnable", 1);
            if (ip_port.Length == 0)
            {
                optionKey.SetValue("ProxyEnable", 0);
            }
            optionKey.SetValue("ProxyServer", ip_port);

            //激活代理设置
            InternetSetOption(0, 39, IntPtr.Zero, 0);
            InternetSetOption(0, 37, IntPtr.Zero, 0);
        }

        /// <summary>
        /// 不使用代理设置
        /// </summary>
        public static void UnSetProxy()
        {
            RegistryKey regKey = Registry.CurrentUser;
            string SubKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";
            RegistryKey optionKey = regKey.OpenSubKey(SubKeyPath, true);
            //更改健值，设置代理，
            optionKey.SetValue("ProxyEnable", 0);
            //激活代理设置
            InternetSetOption(0, 39, IntPtr.Zero, 0);
            InternetSetOption(0, 37, IntPtr.Zero, 0);
        }
    }
}

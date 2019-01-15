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
        public static void SpoofIpAddressBySetProxyForSystem(string ip,int port,string url)
        {
            SetProxy($"{ip}:{port}");
            //设置之后，可以查询当前IP
            //在这里发送请求
            var str = new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            UnSetProxy();

            //另解
            //IEProxy ieProxy = new IEProxy($"{ip}:{port}");
            //ieProxy.RefreshIESettings();
            ////在这里发送请求
            //var str= new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            //ieProxy.DisableIEProxy();
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

        #region 设置全局代理IP 1

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
        #endregion

        #region 设置全局代理IP 2

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);
        public struct Struct_INTERNET_PROXY_INFO
        {
            public int dwAccessType;
            public IntPtr proxy;
            public IntPtr proxyBypass;
        };
        //strProxy为代理IP:端口
        private void RefreshIESettings(string strProxy)
        {
            const int INTERNET_OPTION_PROXY = 38;
            const int INTERNET_OPEN_TYPE_PROXY = 3;
            const int INTERNET_OPEN_TYPE_DIRECT = 1;

            Struct_INTERNET_PROXY_INFO struct_IPI;
            // Filling in structure
            struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY;
            struct_IPI.proxy = Marshal.StringToHGlobalAnsi(strProxy);
            struct_IPI.proxyBypass = Marshal.StringToHGlobalAnsi("local");

            // Allocating memory
            IntPtr intptrStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct_IPI));
            if (string.IsNullOrEmpty(strProxy) || strProxy.Trim().Length == 0)
            {
                strProxy = string.Empty;
                struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_DIRECT;

            }
            // Converting structure to IntPtr
            Marshal.StructureToPtr(struct_IPI, intptrStruct, true);

            bool iReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, Marshal.SizeOf(struct_IPI));
        }
        public class IEProxy
        {
            private const int INTERNET_OPTION_PROXY = 38;
            private const int INTERNET_OPEN_TYPE_PROXY = 3;
            private const int INTERNET_OPEN_TYPE_DIRECT = 1;

            private string ProxyStr;


            [DllImport("wininet.dll", SetLastError = true)]

            private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

            public struct Struct_INTERNET_PROXY_INFO
            {
                public int dwAccessType;
                public IntPtr proxy;
                public IntPtr proxyBypass;
            }

            private bool InternetSetOption(string strProxy)
            {
                int bufferLength;
                IntPtr intptrStruct;
                Struct_INTERNET_PROXY_INFO struct_IPI;

                if (string.IsNullOrEmpty(strProxy) || strProxy.Trim().Length == 0)
                {
                    strProxy = string.Empty;
                    struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_DIRECT;
                }
                else
                {
                    struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY;
                }
                struct_IPI.proxy = Marshal.StringToHGlobalAnsi(strProxy);
                struct_IPI.proxyBypass = Marshal.StringToHGlobalAnsi("local");
                bufferLength = Marshal.SizeOf(struct_IPI);
                intptrStruct = Marshal.AllocCoTaskMem(bufferLength);
                Marshal.StructureToPtr(struct_IPI, intptrStruct, true);
                return InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, bufferLength);

            }
            public IEProxy(string strProxy)
            {
                this.ProxyStr = strProxy;
            }
            //设置代理
            public bool RefreshIESettings()
            {
                return InternetSetOption(this.ProxyStr);
            }
            //取消代理
            public bool DisableIEProxy()
            {
                return InternetSetOption(string.Empty);
            }
        }
        #endregion
    }
}

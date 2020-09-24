using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeLibrary.CancleRequest
{
    public class CancleRequestDemo
    {
        public static void CancleRequestByTask(string url)
        {
            //https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-cancellation
            //http://www.cnblogs.com/Mainz/archive/2012/03/21/2410699.html

            var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            var task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        //此时已经取消任务了
                        //token.ThrowIfCancellationRequested();
                    }
                    else
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            var response = client.GetAsync(url).Result;
                            var str = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                    
                }
                
            }, token);

            //等待10s，然后取消任务
            if (!task.Wait(10000, token))
            {
                tokenSource.Cancel();
            }
            //等待循环到token.IsCancellationRequested=true
            Thread.Sleep(5000);
            tokenSource.Dispose();

        }

        public static void CancleRequestByTimeout(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //设置超时时间为1s
                    client.Timeout = new TimeSpan(0, 0, 1);
                    var response = client.GetAsync(url).Result;
                    var str = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch(Exception ex)
            {
                //请求失败，1s后会抛异常到达这里
            }
        }
    }
}

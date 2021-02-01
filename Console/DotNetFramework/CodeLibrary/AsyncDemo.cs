using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net.Http;
using System.Net;

namespace CodeLibrary
{
    public class AsyncDemo
    {

        public async Task AsyncMethod1()
        {
            HttpClient client = new HttpClient();
            await client.GetAsync("https://www.baidu.com/");
        }

        public async Task<byte[]> AsyncMethod2()
        {
            WebClient client = new WebClient();
            return await client.UploadFileTaskAsync("", "");
        }

        public void TaskMethod()
        {
            //Task.Run
            Task.Run(() =>
            {

            });

            Task.Run(async () =>
            {
                await AsyncMethod1();
            });

            //Start
            Task t = new Task(Method1);
            t.Start();

            //Task.Factory.StartNew  创建并启动
            t = Task.Factory.StartNew(Method1);

            Task t1=new Task(Method1), t2 = new Task(Method1), t3 = new Task(Method1);
            Task[] tasks = new Task[] { t1, t2, t3 };
            //等待所有完成
            Task.WaitAll(tasks);
            //等待任意一个完成
            Task.WhenAll(tasks);

            //任务t完成后，调用Method2
            t.ContinueWith(Method2);
        }

        public void Method1()
        {

        }

        public void Method2(Task t)
        {
            Console.WriteLine($"task {t.Id} finished");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.ThreadIDTest
{
    public class ThreadIDTestDemo
    {
        public static void Run()
        {
            Task.Run(async () => { await GetA(); });
            Thread.Sleep(1 * 1000);

            Task.Run(() => { GetB(); });
            Thread.Sleep(1 * 1000);

            ShowTheadInfo(nameof(Run));
        }

        public static async Task GetA()
        {
            ShowTheadInfo(nameof(GetA));
            await Task.Run(() => { return "A"; });
        }

        public static Task GetB()
        {
            ShowTheadInfo(nameof(GetB));
            return Task.Run(() => { return "B"; });
        }

        /// <summary>
        /// 显示线程信息
        /// </summary>
        public static void ShowTheadInfo(string? methodName)
        {
            Thread nowThead = Thread.CurrentThread;

            Console.WriteLine($"方法名:{methodName}");
            Console.WriteLine($"线程Name:{nowThead.Name}");
            Console.WriteLine($"托管线程ID:{nowThead.ManagedThreadId}");
            Console.WriteLine($"托管线程哈希代码:{nowThead.GetHashCode()}");
            Console.WriteLine($"线程IsAlive:{nowThead.IsAlive}");
            Console.WriteLine($"线程是否是线程池的线程IsThreadPoolThread:{nowThead.IsThreadPoolThread}");
            Console.WriteLine($"线程优先级Priority:{nowThead.Priority}");
            Console.WriteLine($"线程状态ThreadState:{nowThead.ThreadState}");
            Console.WriteLine("--------------------");
        }

    }
}

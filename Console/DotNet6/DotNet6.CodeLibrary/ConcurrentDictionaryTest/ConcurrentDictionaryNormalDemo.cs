using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.ConcurrentDictionaryTest
{
    public class ConcurrentDictionaryNormalDemo
    {
        private static int runCount = 0;

        private static readonly ConcurrentDictionary<string, string> cache
            = new ConcurrentDictionary<string, string>();

        public static void Run()
        {
            Task task1 = Task.Run(() => ShowValue("第一个值"));
            Task task2 = Task.Run(() => ShowValue("第二个值"));
            Task.WaitAll(task1, task2);

            ShowValue("第三个值");

            Console.WriteLine($"总共运行: {runCount}");
        }

        public static void ShowValue(string value)
        {
            string valueFound = cache.GetOrAdd(
                key: "key",
                valueFactory: _ =>
                {
                    Interlocked.Increment(ref runCount);
                    Thread.Sleep(10);
                    return value;
                });

            Console.WriteLine(valueFound);
        }
    }
}

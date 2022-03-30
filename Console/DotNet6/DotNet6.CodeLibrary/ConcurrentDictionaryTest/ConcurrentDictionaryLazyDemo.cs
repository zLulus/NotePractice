using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.ConcurrentDictionaryTest
{
    public class ConcurrentDictionaryLazyDemo
    {
        private static int runCount = 0;

        private static readonly ConcurrentDictionary<string, Lazy<string>> cache
            = new ConcurrentDictionary<string, Lazy<string>>();

        public static void Run()
        {
            Task task1 = Task.Run(() => ShowValue2("第一个值"));
            Task task2 = Task.Run(() => ShowValue2("第二个值"));
            Task.WaitAll(task1, task2);

            ShowValue2("第三个值");

            Console.WriteLine($"总共运行: {runCount}");
        }

        public static void ShowValue2(string value)
        {
            var valueFound = cache.GetOrAdd(
                key: "key",
                valueFactory: _ => new Lazy<string>(
                    () =>
                    {
                        Interlocked.Increment(ref runCount);
                        Thread.Sleep(100);
                        return value;
                    })
                );
            Console.WriteLine(valueFound.Value);
        }
    }
}

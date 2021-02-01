using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5.CodeLibrary.Discards
{
    public class DiscardsDemo
    {
        public static async Task Run()
        {
            //使用弃元：使用独立占位符来忽略异步操作返回的 Task 对象，抑制操作即将完成时所引发的异常
            await ExecuteAsyncMethodsWithDiscards();
            //不使用弃元：抛出异常
            //await ExecuteAsyncMethodsWithoutDiscards();
        }

        public static async Task ExecuteAsyncMethodsWithDiscards()
        {
            Console.WriteLine("About to launch a task...");
            _ = Task.Run(() => {
                var iterations = 0;
                for (int ctr = 0; ctr < int.MaxValue; ctr++)
                    iterations++;
                Console.WriteLine("Completed looping operation...");
                throw new InvalidOperationException();
            });
            await Task.Delay(5000);
            Console.WriteLine("Exiting after 5 second delay");
        }

        public static async Task ExecuteAsyncMethodsWithoutDiscards()
        {
            Console.WriteLine("About to launch a task...");
            await Task.Run(() => {
                var iterations = 0;
                for (int ctr = 0; ctr < int.MaxValue; ctr++)
                    iterations++;
                Console.WriteLine("Completed looping operation...");
                throw new InvalidOperationException();
            });
            await Task.Delay(5000);
            Console.WriteLine("Exiting after 5 second delay");
        }
    }
}

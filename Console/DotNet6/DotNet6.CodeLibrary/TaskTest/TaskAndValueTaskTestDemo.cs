using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.TaskTest
{
    public static class TaskAndValueTaskTestDemo
    {
        static int count = 0;
        public static async void Run()
        {
            var task = GetResult();
            var r1 = await task;
            var r2 = await task;

            var task2 = GetResultForValueTask();
            while (!task2.IsCompleted)
            {
                await Task.Delay(1);
            }
            var r3 = await task2;
            var r4 = await task2;
        }

        private static async Task<bool> GetResult()
        {
            await Task.CompletedTask;
            return count++%2==0;
        }

        private static async ValueTask<bool> GetResultForValueTask()
        {
            await Task.CompletedTask;
            return count++ % 2 == 0;
        }
    }
}

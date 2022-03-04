using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.TaskTest
{
    public class TaskAwaiterTestDemo
    {
        public static void Run()
        {
            var a = new Task(() => { }).GetAwaiter();
            var b = new Task(() => { }).ConfigureAwait(false).GetAwaiter();

            var a1 = new Task(() => { }).ContinueWith(t => { }).GetAwaiter();

            //var taskScheduler = TaskScheduler.Current;

            //var b1 = new Task(() => { }).ContinueWith(t => { }, TaskScheduler.FromCurrentSynchronizationContext())
            //    .GetAwaiter();
        }
    }
}

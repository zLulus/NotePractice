using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.ConcurrentTest
{
    public static class ConcurrentTestDemo
    {
        public static async Task Run()
        {
            //https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.list-1?view=net-6.0&WT.mc_id=DT-MVP-5003010
            await ListTest();

            //https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentbag-1?view=net-6.0&WT.mc_id=DT-MVP-5003010
            await ConcurrentBagTest();

            //https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.blockingcollection-1?view=net-6.0&WT.mc_id=DT-MVP-5003010
            await BlockingCollectionTest();
        }

        private static async Task BlockingCollectionTest()
        {
            int num=0;
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                Task[] tasks = new Task[10];
                // Spin up a Task to populate the BlockingCollection
                for (int i = 0; i < 10; i++)
                {
                    tasks[i]= Task.Run(() =>
                    {
                        bc.Add(num++);
                    });
                }

                // Spin up a Task to consume the BlockingCollection
                Task t2 = Task.Run(() =>
                {
                    try
                    {
                        // Consume consume the BlockingCollection
                        while (true) Console.WriteLine(bc.Take());
                    }
                    catch (InvalidOperationException)
                    {
                        // An InvalidOperationException means that Take() was called on a completed collection
                        Console.WriteLine("That's All!");
                    }
                });

                await Task.WhenAll(tasks);

                bc.CompleteAdding();

                Task.WaitAll(t2);
            }
        }

        private static async Task ConcurrentBagTest()
        {
            int num = 0;
            ConcurrentBag<int> bag=new ConcurrentBag<int>();

            for (int i = 0; i < 10; i++)
            {
                Task.Run(() =>
                {
                    bag.Add(num++);
                });
            }

            foreach (var data in bag)
                Console.WriteLine(data);
        }

        private static async Task ListTest()
        {
            List<int> list=new List<int>(10);

            //ok
            for (int i = 0; i < 10; i++)
            {
                await Task.Run(() =>
                {
                    list.Add(i);
                });
            }

            //Collection was modified; enumeration operation may not execute.
            //for (int i = 0; i < 10; i++)
            //{
            //    var t= Task.Run(() => { list.Add(i); });
            //}

            //Collection was modified; enumeration operation may not execute.
            //for (int i = 0; i < 10; i++)
            //{
            //    new Thread(() => { list.Add(i); }).Start();
            //}

            foreach (var data in list)
                Console.WriteLine(data);
        }
    }
}

using System;
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
            await ListTest();
        }

        private static async Task ListTest()
        {
            List<int> list=new List<int>();
            //ok
            //for (int i = 0; i < 10; i++)
            //{
            //    await Task.Run(() =>
            //    {
            //        list.Add(i);
            //    });
            //}

            //Collection was modified; enumeration operation may not execute.
            //for (int i = 0; i < 10; i++)
            //{
            //    var t= Task.Run(() => { list.Add(i); });
            //}

            //Collection was modified; enumeration operation may not execute.
            for (int i = 0; i < 10; i++)
            {
                new Thread(() => { list.Add(i); }).Start();
            }



            foreach (var data in list)
                Console.WriteLine(data);
        }
    }
}

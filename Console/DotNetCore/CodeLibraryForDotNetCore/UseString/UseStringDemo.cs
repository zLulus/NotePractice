using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace CodeLibraryForDotNetCore.UseString
{
    public class UseStringDemo
    {
        public static void Run()
        {
            string str1 = "I am a number";
            string str2 = str1;
            Console.WriteLine("str1 = " + str1);//I am a number
            Console.WriteLine("str2 = " + str2);//I am a number

            str1 = "I am another number";
            Console.WriteLine("after str1 changed... str1 = " + str1);//I am another number
            Console.WriteLine("after str1 changed... str2 = " + str2);//I am a number

            string str3 = "I am a number";
            bool r = str2 == str3;
            bool r2 = str2.Equals(str3);
            bool r3 = Object.ReferenceEquals(str2, str3);
            Console.WriteLine(r);//true
            Console.WriteLine(r2);//true
            Console.WriteLine(r3);//true
        }

        public static void Run2()
        {
            var count = 100000;
            var addStr = "1234567890";
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                stringBuilder.Append(addStr);
            }
            var a = stringBuilder.ToString();
            stopwatch.Stop();
            var t1= stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"StringBuilder耗时:{t1}");

            stopwatch.Restart();
            var s = "";
            for (int i = 0; i < count; i++)
            {
                s += addStr;
            }
            stopwatch.Stop();
            var t2 = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"String耗时:{t2}");
        }
    }
}

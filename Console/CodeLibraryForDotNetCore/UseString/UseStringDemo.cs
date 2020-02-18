using System;
using System.Collections.Generic;
using System.Text;

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
            Console.WriteLine(r);//true
            Console.WriteLine(r2);//true
        }
    }
}

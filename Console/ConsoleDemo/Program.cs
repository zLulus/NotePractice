using CodeLibrary;
using CodeLibrary.ExcuteJs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //EnumTest();

            //在正则表达式中给取得的值命名
            //RegexDemo.Demo();

            //关闭之前进行一些操作
            //handler = new ConsoleEventDelegate(ConsoleEventCallback);
            //SetConsoleCtrlHandler(handler, true);
            //Console.ReadLine();

            //C# 元组、匿名对象、ref&out
            //ReturnsMultipleValuesTest test = new ReturnsMultipleValuesTest();
            //test.Test();

            //C#调用js Jint库
            ExcuteJsDemo.ExcuteJs();
        }

        #region 关闭之前进行一些操作
        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                //这是关闭之前
                Console.WriteLine("Console window closing, death imminent");
            }
            return false;
        }
        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected
                                               // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

        #endregion

        private static void EnumTest()
        {
            int i = 0;
            Console.WriteLine(i);
            Console.WriteLine(i.ToString());
            Console.WriteLine();

            Student s = new Student() {Name = "YYY"};
            Console.WriteLine(s);
            Console.WriteLine(s.ToString());
            Console.WriteLine();

            Console.WriteLine(Days.Friday);
            Console.WriteLine(Days.Friday.ToString());
            Console.WriteLine((int) Days.Friday);

            Console.Read();
        }
    }

    public enum Days
    {
        Sunday, Monday, Tuesday, Wednesday, Thursday, Friday=2, Saturday
    }

    public class Student
    {
        public string Name { get; set; }
    }
}

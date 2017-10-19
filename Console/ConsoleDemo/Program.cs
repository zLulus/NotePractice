using CodeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
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
            RegexDemo.Demo();
        }

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

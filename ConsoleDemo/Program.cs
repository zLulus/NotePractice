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
            Student s = new Student() { Name = "YYY",Number="0" };
            Test.SetNumber(s);
            Console.Read();
            //NewMethod();
        }

        private static void NewMethod()
        {
            int i = 0;
            Console.WriteLine(i);
            Console.WriteLine(i.ToString());
            Console.WriteLine();

            Student s = new Student() { Name = "YYY" };
            Console.WriteLine(s);
            Console.WriteLine(s.ToString());
            Console.WriteLine();

            Console.WriteLine(Days.Friday);
            Console.WriteLine(Days.Friday.ToString());
            Console.WriteLine((int)Days.Friday);

            Console.Read();
        }
    }

    public class Student
    {
        public string Number { get; set; }
        public string Name { get; set; }
    }

    public enum Days
    {
        Sunday, Monday, Tuesday, Wednesday, Thursday, Friday=2, Saturday
    }
}

using DotNet5.CodeLibrary.LambdaTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5.CodeLibrary.LambdaTests
{
    public class LambdaLifeCycleDemo
    {
        delegate void AddInsideX();
        delegate void AddOutsideX(int x);
        delegate void AddInsideClass();
        delegate void AddOutsideClass(Student x);

        public static void Run()
        {
            AddInsideX fun = delegate ()
            {
                int x = 0;
                x++;
                Console.WriteLine(x);
            };

            fun();
            fun();
        }

        public static void Run2()
        {
            int x = 0;
            AddOutsideX fun = delegate (int x)
            {
                x++;
                Console.WriteLine(x);
            };

            fun(x);
            fun(x);
        }

        public static void Run3()
        {
            AddOutsideX fun2 = delegate (int x)
            {
                x=x++;
            };
            AddInsideX fun = delegate ()
            {
                int x = 0;
                fun2(x);
                Console.WriteLine(x);
            };

            fun();
            fun();
        }

        public static void Run4()
        {
            AddOutsideClass fun2 = delegate (Student s)
            {
                s.Age = s.Age+1;
            };
            AddInsideClass fun = delegate ()
            {
                Student student = new Student() { Age = 0 };
                fun2(student);
                Console.WriteLine(student.Age);
            };

            fun();
            fun();
        }

        public static void Run5()
        {
            Student student = new Student() { Age = 0 };
            AddOutsideClass fun2 = delegate (Student s)
            {
                s.Age = s.Age + 1;
            };

            fun2(student);
            Console.WriteLine($"{student.Age}");
            fun2(student);
            Console.WriteLine($"{student.Age}");
        }
    }
}

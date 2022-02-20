using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5.CodeLibrary.TestRefAndOut
{
    public class TestRefAndOutDemo
    {
        public static void Run()
        {
            Animal animal = new Animal
            {
                Name = "A"
            };

            Test1(animal);

            Console.WriteLine(animal.Name); // 输出B

            Animal animal2 = new Animal
            {
                Name = "A"
            };

            Test2(ref animal2);

            Console.WriteLine(animal2.Name); // 输出C
        }

        static void Test1(Animal obj)
        {
            obj.Name = "B";
            obj = new Animal
            {
                Name = "C"
            };
        }

        static void Test2(ref Animal obj)
        {
            obj.Name = "B";
            obj = new Animal
            {
                Name = "C"
            };
        }
    }

    internal class Animal
    {
        internal string Name { get; set; }
    }
}

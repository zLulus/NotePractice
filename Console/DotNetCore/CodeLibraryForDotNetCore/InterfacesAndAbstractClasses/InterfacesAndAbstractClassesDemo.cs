using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.InterfacesAndAbstractClasses
{
    public class InterfacesAndAbstractClassesDemo
    {
        public static void Run()
        {
            Console.WriteLine("*** man class ***");
            var man = new Man("Ben");
            man.Live();
            man.Walk();
            man.Walk(1.1);
            man.Eat(10);
            man.Eat("麻婆豆腐");

            Console.WriteLine("*** woman class ***");
            var woman = new Woman("Jenny");
            woman.Live();
            woman.Walk();
            woman.Walk(0.5);
            woman.Eat(8);
            woman.Eat("麻辣牛肉");

            Console.WriteLine("*** cat class ***");
            var cat = new Cat();
            cat.Live();
            cat.Walk();
            cat.Walk(0.5);
            cat.Eat(8);
            cat.Eat("麻辣牛肉");
        }
    }
}

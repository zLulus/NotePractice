using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.InterfacesAndAbstractClasses
{
    public class Cat : Biological, IWalk, IEat
    {

        public void Eat(decimal money)
        {
            Console.WriteLine($"吃饭花费了{money}.");
        }

        public void Eat(string dishName)
        {
            Console.WriteLine($"吃了{dishName}.");
        }

        public void Walk()
        {
            Console.WriteLine($"走路.");
        }

        public void Walk(double weightBearing)
        {
            Console.WriteLine($"负重{weightBearing}走路.");
        }
    }
}

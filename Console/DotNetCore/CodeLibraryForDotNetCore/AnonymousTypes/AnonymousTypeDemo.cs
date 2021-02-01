using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.AnonymousTypes
{
    public class AnonymousTypeDemo
    {
        public static void Run()
        {
            var anonArray = new[] { new { name = "apple", diam = 4 }, new { name = "apple", diam = 4 }, new { name = "grape", diam = 1 } };
            var isEquals = anonArray[0] == anonArray[1];//false
            Type type = anonArray[0].GetType();
            Type type2 = anonArray[1].GetType();
        }
    }
}

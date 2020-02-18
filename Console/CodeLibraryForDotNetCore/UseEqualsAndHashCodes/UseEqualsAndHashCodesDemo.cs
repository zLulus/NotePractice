using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibraryForDotNetCore.UseEqualsAndHashCodes
{
    public class UseEqualsAndHashCodesDemo
    {
        public static void Run()
        {
            var t1 = new Task(() => { });
            var t2 = t1;
            var t3 = new Task(() => { });
            Console.WriteLine($"t1:{t1.GetHashCode()}");
            Console.WriteLine($"t2:{t2.GetHashCode()}");
            Console.WriteLine($"t3:{t3.GetHashCode()}");
        }
    }
}

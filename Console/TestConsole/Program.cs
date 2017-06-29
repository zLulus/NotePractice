using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine(i);
            }
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine(i);
            }
            Console.Read();
        }
    }
}

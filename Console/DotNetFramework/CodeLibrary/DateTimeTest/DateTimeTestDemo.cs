using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.DateTimeTest
{
    public class DateTimeTestDemo
    {
        public static void Run()
        {
            Console.WriteLine($"{DateTime.Now == DateTime.Now}");
            Console.WriteLine($"{DateTime.Now.Ticks == DateTime.Now.Ticks}");
            Console.WriteLine($"{DateTime.UtcNow == DateTime.UtcNow}");
            Console.WriteLine($"{DateTime.UtcNow.Ticks == DateTime.UtcNow.Ticks}");
            Console.WriteLine($"{ GetTicks(DateTime.Now)},{GetTicks(DateTime.Now)}");
            Console.WriteLine($"{ GetTicks(DateTime.UtcNow)},{GetTicks(DateTime.UtcNow)}");
        }



        public static long GetTicks(DateTime t1)
        {
            return new DateTimeOffset(t1).ToUnixTimeSeconds();
        }
    }
}

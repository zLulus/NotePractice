using Microsoft.Extensions.Configuration;
using System;

namespace CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigReadDemo.ReadConfig();

            Console.ReadLine();
        }
    }
}

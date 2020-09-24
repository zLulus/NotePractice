using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseTry
{
    public class UseTryDemo
    {
        public static void Run()
        {
            Console.WriteLine(GetNumber());
            Console.ReadLine();
        }

        private static int GetNumber()
        {
            int i = 0;
            try
            {
                return i;
            }
            finally
            {
                Console.WriteLine("finally");
                //i++;
                ++i;
            }
        }
    }
}

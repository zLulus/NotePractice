using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.DelegateAndEvent.Event
{
    public class EventDemo
    {
        private static void EnglishGreeting(string name)
        {
            Console.WriteLine("Morning, " + name);
        }

        private static void ChineseGreeting(string name)
        {
            Console.WriteLine("早上好, " + name);
        }

        public static void Run()
        {
            GreetingManager GreetingManager = new GreetingManager();
            GreetingManager.AddMakeGreet(EnglishGreeting);
            GreetingManager.AddMakeGreet(ChineseGreeting);
            GreetingManager.GreetPeople("Lulu");
        }
    }
}

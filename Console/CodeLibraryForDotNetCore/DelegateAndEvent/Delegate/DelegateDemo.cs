using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.DelegateAndEvent.Delegate
{
    public class DelegateDemo
    {
        private delegate void GreetingDelegate(string name);
        private static void EnglishGreeting(string name)
        {
            Console.WriteLine("Morning, " + name);
        }

        private static void ChineseGreeting(string name)
        {
            Console.WriteLine("早上好, " + name);
        }

        //注意此方法，它接受一个GreetingDelegate类型的方法作为参数
        private static void GreetPeople(string name, GreetingDelegate MakeGreeting)
        {
            MakeGreeting(name);
        }

        public static void DelegateDemo1()
        {
            GreetPeople("Lulu", DelegateDemo.EnglishGreeting);
            GreetPeople("Lulu2", DelegateDemo.ChineseGreeting);
        }

        public static void DelegateDemo2()
        {
            GreetingDelegate greetingDelegate1, greetingDelegate2;
            greetingDelegate1 = EnglishGreeting;
            greetingDelegate2 = ChineseGreeting;

            GreetPeople("Lulu", greetingDelegate1);
            GreetPeople("Lulu2", greetingDelegate2);
        }

        public static void DelegateDemo3()
        {
            GreetingDelegate greetingDelegate1;
            // 先给委托类型的变量赋值
            greetingDelegate1 = EnglishGreeting;
            // 给此委托变量再绑定一个方法
            greetingDelegate1 += ChineseGreeting;
            // 将先后调用 EnglishGreeting 与 ChineseGreeting 方法
            GreetPeople("Lulu", greetingDelegate1);
        }

        public static void DelegateDemo4()
        {
            GreetingDelegate greetingDelegate1;
            // 先给委托类型的变量赋值
            greetingDelegate1 = EnglishGreeting;
            // 给此委托变量再绑定一个方法
            greetingDelegate1 += ChineseGreeting;
            //取消对EnglishGreeting方法的绑定
            greetingDelegate1 -= EnglishGreeting;
            // 将调用ChineseGreeting 方法
            GreetPeople("Lulu", greetingDelegate1);
        }


    }
}

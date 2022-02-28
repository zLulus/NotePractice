using System.Diagnostics;
using System.Reflection;

namespace DotNet6.CodeLibrary.ReflectionPerformanceTest
{
    public class MyClass
    {
        public int Number { get; set; }
    }

    public class ReflectionPerformanceTestDemo
    {
        private static int Count = 10000000;
        public static void Run()
        {
            //https://www.c-sharpcorner.com/article/boosting-up-the-reflection-performance-in-c-sharp/
            Test1();
            Test2();
            Test3();

            //speed 速度
            //Test3>Test1>Test2
        }

        private static void Test3()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<MyClass> myClassList = Enumerable.Repeat(new MyClass(), Count).ToList();
            object aux = 0;

            Action<MyClass, int> setter = (Action<MyClass, int>)Delegate.CreateDelegate(typeof(Action<MyClass, int>), null, typeof(MyClass).GetProperty("Number").GetSetMethod());

            Func<MyClass, int> getter = (Func<MyClass, int>)Delegate.CreateDelegate(typeof(Func<MyClass, int>), null, typeof(MyClass).GetProperty("Number").GetGetMethod());

            foreach (var obj in myClassList)
            {
                aux = getter(obj);
                setter(obj, 3);
            }

            sw.Stop();
            Console.WriteLine($"{nameof(Test3)}: {sw.Elapsed.TotalMilliseconds}ms");
        }

        private static void Test2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<MyClass> myClassList = Enumerable.Repeat(new MyClass(), Count).ToList();
            object aux = 0;

            PropertyInfo info = typeof(MyClass).GetProperty(nameof(MyClass.Number));

            foreach (var obj in myClassList)
            {
                aux = info.GetValue(obj);
                info.SetValue(obj, 3);
            }

            sw.Stop();
            Console.WriteLine($"{nameof(Test2)}: {sw.Elapsed.TotalMilliseconds}ms");
        }

        private static void Test1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<MyClass> myClassList = Enumerable.Repeat(new MyClass(), Count).ToList();
            object aux = 0;

            foreach (var obj in myClassList)
            {
                aux = obj.Number;
                obj.Number = 3;
            }

            sw.Stop();
            Console.WriteLine($"{nameof(Test1)}: {sw.Elapsed.TotalMilliseconds}ms");
        }
    }
}

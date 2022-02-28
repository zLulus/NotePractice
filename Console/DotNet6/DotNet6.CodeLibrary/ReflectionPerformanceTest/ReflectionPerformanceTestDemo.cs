using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace DotNet6.CodeLibrary.ReflectionPerformanceTest
{
    public class MyClass
    {
        public int Number { get; set; }
    }

    public class ReflectionPerformanceTestDemo
    {
        public static void Run()
        {
            //https://www.c-sharpcorner.com/article/boosting-up-the-reflection-performance-in-c-sharp/
            //https://stackoverflow.com/questions/1027980/improving-performance-reflection-what-alternatives-should-i-consider
            //https://github.com/mgravell/fast-member

            int count = 10000000;
            Test1(count);
            Test2(count);
            Test3(count);
            Test4(count);
            Test5(count);

            Console.WriteLine();

            count = 100000000;
            Test1(count);
            Test2(count);
            Test3(count);
            Test4(count);
            Test5(count);

            Console.WriteLine();

            //too slow
            //count = 1000000000;
            //Test1(count);
            //Test2(count);
            //Test3(count);
            //Test4(count);

        }

        private static void Test5(int count)
        {
            List<MyClass> myClassList = Enumerable.Repeat(new MyClass(), count).ToList();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            //https://stackoverflow.com/questions/9601707/how-to-set-property-value-using-expressions
            Expression<Func<MyClass, int>> memberLamda = x => x.Number;
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null)
                {
                    foreach (var obj in myClassList)
                        property.SetValue(obj, 3, null);
                }
            }
            sw.Stop();
            Console.WriteLine($"{nameof(Test5)}(count:{count}): {sw.Elapsed.TotalMilliseconds}ms");
        }

        private static void Test4(int count)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(MyClass));
            List<MyClass> myClassList = Enumerable.Repeat(new MyClass(), count).ToList();
            object aux = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (var obj in myClassList)
            {
                aux= props[nameof(MyClass.Number)].GetValue(obj);
                props[nameof(MyClass.Number)].SetValue(obj, 3);
            }

            sw.Stop();
            Console.WriteLine($"{nameof(Test4)}(count:{count}): {sw.Elapsed.TotalMilliseconds}ms");
        }

        private static void Test3(int count)
        {
            List<MyClass> myClassList = Enumerable.Repeat(new MyClass(), count).ToList();
            object aux = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Action<MyClass, int> setter = (Action<MyClass, int>)Delegate.CreateDelegate(typeof(Action<MyClass, int>), null, typeof(MyClass).GetProperty(nameof(MyClass.Number)).GetSetMethod());
            Func<MyClass, int> getter = (Func<MyClass, int>)Delegate.CreateDelegate(typeof(Func<MyClass, int>), null, typeof(MyClass).GetProperty(nameof(MyClass.Number)).GetGetMethod());

            foreach (var obj in myClassList)
            {
                aux = getter(obj);
                setter(obj, 3);
            }

            sw.Stop();
            Console.WriteLine($"{nameof(Test3)}(count:{count}): {sw.Elapsed.TotalMilliseconds}ms");
        }

        private static void Test2(int count)
        {
            List<MyClass> myClassList = Enumerable.Repeat(new MyClass(), count).ToList();
            object aux = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            PropertyInfo info = typeof(MyClass).GetProperty(nameof(MyClass.Number));

            foreach (var obj in myClassList)
            {
                aux = info.GetValue(obj);
                info.SetValue(obj, 3);
            }

            sw.Stop();
            Console.WriteLine($"{nameof(Test2)}(count:{count}): {sw.Elapsed.TotalMilliseconds}ms");
        }

        private static void Test1(int count)
        {
            List<MyClass> myClassList = Enumerable.Repeat(new MyClass(), count).ToList();
            object aux = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (var obj in myClassList)
            {
                aux = obj.Number;
                obj.Number = 3;
            }

            sw.Stop();
            Console.WriteLine($"{nameof(Test1)}(count:{count}): {sw.Elapsed.TotalMilliseconds}ms");
        }
    }
}

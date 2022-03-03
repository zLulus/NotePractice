using System.Reflection;

namespace DotNet6.CodeLibrary.HigherOrderFunctionTest
{
    public static class HigherOrderFunctionTestDemo
    {
        public static Func<T2, T1, R> Swap<T1, T2, R>(this Func<T1, T2, R> f) => (t2, t1) => f(t1, t2);

        public static void Run()
        {
            //https://functionalprogrammingcsharp.com/higher-order-functions
            var numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var even = numbers.Where(x => x % 2 == 0);
            // => [2, 4, 6, 8]
            //lambda表达式
            Func<int, bool> isOdd = x => x % 2 != 0; // assign lambda expression to Func
            var odd = numbers.Where(isOdd);
            // => [1, 3, 5, 7, 9]



            Func<int, bool> isDivisible(int n)
            {
                return x => x % n == 0;
            }

            var numbers2 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var num3 = numbers2.Where(isDivisible(3));
            // => [3, 6, 9]




            Func<int, int, int> divide = (x, y) => x / y;
            divide(6, 2); // => 3

            //交换参数位置
            var divideBy = divide.Swap();
            divideBy(2, 6); // => 3
        }
    }
}

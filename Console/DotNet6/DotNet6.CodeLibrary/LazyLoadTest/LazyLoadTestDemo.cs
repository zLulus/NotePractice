using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.LazyLoadTest
{
    public class LazyLoadTestDemo
    {
        public static void Run()
        {
            Func<int[]> getNumbers = () =>
            {
                Console.WriteLine("加载数据方法执行一次");
                return new[] { 1, 2, 3, 4, 5, 6, 7 };
            };
            Lazy<int[]> lazyData = new Lazy<int[]>(getNumbers,true);
            Console.WriteLine("是否初始化 = " + lazyData.IsValueCreated);
            var sum= lazyData.Value.Sum();//此处访问时才会真正的初始化
            Console.WriteLine("是否初始化 = " + lazyData.IsValueCreated);


        }
    }
}

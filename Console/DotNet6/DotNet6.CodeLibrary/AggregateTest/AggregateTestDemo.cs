using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.AggregateTest
{
    public class AggregateTestDemo
    {
        public static void Run()
        {
            int[] sourceInts = new int[] { 1, 2, 3, 4 };
            int seed = 10;
            Expression<Func<int, int, int>> aggregateFunc = (a, b) => a * b;
            Expression<Func<int, int>> resultSelector = a => a + 10000;

            //https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.aggregate?view=net-6.0&WT.mc_id=DT-MVP-5003010
            //起始种子10*数组1*2*3*4+结果映射函数10000
            int result = System.Linq.Enumerable.Aggregate(
                  source: sourceInts,
                  seed: seed,
                  func: aggregateFunc.Compile(),
                  resultSelector: resultSelector.Compile()
                  );

            Console.WriteLine("源数组：" + JsonConvert.SerializeObject(sourceInts));
            Console.WriteLine("起始种子：" + seed);
            Console.WriteLine("累加函数：" + aggregateFunc.ToString());
            Console.WriteLine("结果映射函数：" + resultSelector.ToString());
            Console.WriteLine("结果：" + result);
        }
    }
}

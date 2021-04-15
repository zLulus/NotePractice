using DotNet5.CodeLibrary.ArraySegments;
using DotNet5.CodeLibrary.Discards;
using DotNet5.CodeLibrary.Traverse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNet5Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //弃元
            //https://docs.microsoft.com/zh-cn/dotnet/csharp/discards?WT.mc_id=DT-MVP-5003010
            //await DiscardsDemo.Run();

            //await ArraySegmentDemo.Run();

            //Console.WriteLine("List集合正序/倒序操作:");
            //TraverseListDemo.Run();
            Console.WriteLine("ArrayList集合正序/倒序操作:");
            TraverseArrayListDemo.Run();

            Console.ReadLine();
        }
    }
}

using DotNet5.CodeLibrary.ArraySegments;
using DotNet5.CodeLibrary.Discards;
using DotNet5.CodeLibrary.LambdaTests;
using DotNet5.CodeLibrary.Traverse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNet5.CodeLibrary.TestRefAndOut;

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
            //Console.WriteLine("ArrayList集合正序/倒序操作:");
            //TraverseArrayListDemo.Run();

            //lambda和闭包
            //lambda == 匿名函数
            //在返回函数中引用外部函数的参数或局部变量的程序结构，我们称为闭包
            //闭包和 lambda 经常一起出现
            //匿名函数会拷贝一份参数到函数对象中
            //LambdaLifeCycleDemo.Run();
            //LambdaLifeCycleDemo.Run2();
            //LambdaLifeCycleDemo.Run3();
            //LambdaLifeCycleDemo.Run4();
            //LambdaLifeCycleDemo.Run5();

            //值传递和引用传递
            //https://www.cnblogs.com/eventhorizon/p/10357576.html
            TestRefAndOutDemo.Run();

            Console.ReadLine();
        }
    }
}

using DotNet5.CodeLibrary.Discards;
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
            await DiscardsDemo.Run();

            Console.ReadLine();
        }
    }
}

using CodeLibraryForDotNetCore;
using CodeLibraryForDotNetCore.SendEmail;
using CodeLibraryForDotNetCore.UsePostgresql;
using CodeLibraryForDotNetCore.UseYield;
using CoreConsole;
using System;
using System.Threading.Tasks;
using CodeLibraryForDotNetCore.Extensions1;
using CodeLibraryForDotNetCore.Extensions2;
using IsNullOrEmptyCustomExtension= CodeLibraryForDotNetCore.Extensions2.StringExtension2;

namespace DotNetCoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //redis
            //RedisDemo.Run();

            //postgresql
            //Task.Run(async () =>
            //{
            //    await (new UsePostgresqlDemo(new TestDbContextFactory().CreateDbContext(args))).Run();
            //});

            //read config
            //ConfigReadDemo.ReadConfig();
            //ConfigReadDemo.ReadConfigByBind();
            //ConfigReadDemo.ReadConfigHotUpdate();

            //send email
            //SendEmailDemo.Run();

            //yield
            //UseYieldDemo.Run();

            //扩展方法同名
            var str = "Test Extensions";
            bool r1= CodeLibraryForDotNetCore.Extensions2.StringExtension2.IsNullOrEmptyCustomExtension(str);
            bool r2= CodeLibraryForDotNetCore.Extensions1.StringExtension1.IsNullOrEmptyCustomExtension(str);
            //如果同时引用了两个命名空间，无法写的效果
            //bool r = str.IsNullOrEmptyCustomExtension();

            Console.ReadLine();
        }

        
    }
}

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
using CodeLibraryForDotNetCore.LinqTools;
using CodeLibraryForDotNetCore.GenerateRegularExpressionByCode.UseCSharpVerbalExpressions;
using CodeLibraryForDotNetCore.UseTcpSocket;
using CodeLibraryForDotNetCore.UseArray;
using CodeLibraryForDotNetCore.StringConverters;
using CodeLibraryForDotNetCore.UseRabbitMQ;

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
            //var str = "Test Extensions";
            //bool r1= CodeLibraryForDotNetCore.Extensions2.StringExtension2.IsNullOrEmptyCustomExtension(str);
            //bool r2= CodeLibraryForDotNetCore.Extensions1.StringExtension1.IsNullOrEmptyCustomExtension(str);
            //如果同时引用了两个命名空间，无法写的效果
            //bool r = str.IsNullOrEmptyCustomExtension();

            //Postgresql 使用空间数据
            //Task.Run(async () =>
            //{
            //    await (new PostgresqlUseGeometryDemo(new TestDbContextFactory().CreateDbContext(args))).Run();
            //});

            //linq 使用lambda表达式
            //new LinqToolDemo(new TestDbContextFactory().CreateDbContext(args)).Run();

            //使用代码动态生成正则表达式
            //UseCSharpVerbalExpressionsDemo.Run();

            //tcp socket demo
            //UseTcpSocketDemo.Run();

            //数组
            //UseArrayDemo.Run();

            //byte to string
            //StringDemo.Run();

            //RabbitMQ
            UseRabbitMQDemo.Run();

            Console.ReadLine();
        }

        
    }
}

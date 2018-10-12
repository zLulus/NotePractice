using CodeLibraryForDotNetCore;
using CodeLibraryForDotNetCore.SendEmail;
using CodeLibraryForDotNetCore.UsePostgresql;
using CodeLibraryForDotNetCore.UseYield;
using CoreConsole;
using System;
using System.Threading.Tasks;

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
            UseYieldDemo.Run();

            Console.ReadLine();
        }

        
    }
}

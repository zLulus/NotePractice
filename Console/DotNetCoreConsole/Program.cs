using CodeLibraryForDotNetCore;
using CodeLibraryForDotNetCore.UsePostgresql;
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
            Task.Run(async () =>
            {
                await (new UsePostgresqlDemo(new TestDbContextFactory().CreateDbContext(args))).Run();
            });
            Console.ReadLine();
        }

        
    }
}

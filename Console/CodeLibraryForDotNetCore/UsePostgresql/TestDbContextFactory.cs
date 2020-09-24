using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UsePostgresql
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/cli/dbcontext-creation
    /// </summary>
    public class TestDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        public TestDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
            //appsettings.json设置为“始终复制”
            var builder = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var connection = configuration.GetConnectionString("Default");
            optionsBuilder.UseNpgsql(connection,
                //要设置NetTopologySuite插件，请将Npgsql.EntityFrameworkCore.PostgreSQL.NetTopologySuite nuget添加到您的项目中
                //To set up the NetTopologySuite plugin, add the Npgsql.EntityFrameworkCore.PostgreSQL.NetTopologySuite nuget to your project
                o => o.UseNetTopologySuite());

            return new TestDbContext(optionsBuilder.Options);
        }
    }
}

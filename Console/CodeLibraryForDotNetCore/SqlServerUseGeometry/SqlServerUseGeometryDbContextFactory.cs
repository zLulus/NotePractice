using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.SqlServerUseGeometry
{
    public class SqlServerUseGeometryDbContextFactory : IDesignTimeDbContextFactory<SqlServerUseGeometryDbContext>
    {
        public SqlServerUseGeometryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlServerUseGeometryDbContext>();
            var builder = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            //查询sql server数据库的连接字符串
            var connection = configuration.GetConnectionString("SqlServerDbConnection");
            optionsBuilder.UseNpgsql(connection);

            return new SqlServerUseGeometryDbContext(optionsBuilder.Options);
        }
    }
}

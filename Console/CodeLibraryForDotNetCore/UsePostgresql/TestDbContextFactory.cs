using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=12345679;CommandTimeout=20;");

            return new TestDbContext(optionsBuilder.Options);
        }
    }
}

using CodeLibrary.UsePostgresql.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.UsePostgresql
{
    public class TestDbContext : DbContext
    {
        public IDbSet<Cat> Cats { get; set; }

        public TestDbContext() : base("Default")
        {

        }

        public TestDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //设置默认模式，ef默认是dbo
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}

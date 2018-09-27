using CodeLibraryForDotNetCore.UsePostgresql.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace CodeLibraryForDotNetCore.UsePostgresql
{
    public class TestDbContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //设置默认模式，ef默认是dbo
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}

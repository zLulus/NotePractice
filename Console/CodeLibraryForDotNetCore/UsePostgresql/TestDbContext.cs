using CodeLibraryForDotNetCore.UsePostgresql.Models;
using CodeLibraryForDotNetCore.UsePostgresql.Models.UseGeometry;
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
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Road> Roads { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //此外，要确保在数据库中安装PostGIS扩展，请将以下内容添加到DbContext：
            //In addition, to make sure that the PostGIS extension is installed in your database, add the following to your DbContext:
            modelBuilder.HasPostgresExtension("postgis");
            //设置默认模式，ef默认是dbo
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}

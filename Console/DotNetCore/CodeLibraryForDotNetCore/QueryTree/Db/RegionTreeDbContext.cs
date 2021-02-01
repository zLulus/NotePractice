using CodeLibraryForDotNetCore.QueryTree.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.QueryTree.Db
{
    public class RegionTreeDbContext : DbContext
    {
        private string connectionString;
        public DbSet<RegionTree> Regions { get; set; }

        public RegionTreeDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}

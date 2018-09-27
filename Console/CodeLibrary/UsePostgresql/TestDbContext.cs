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
            base.OnModelCreating(modelBuilder);
        }
    }
}

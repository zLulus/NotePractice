using DotNetCoreWithFullFramework.DataBases.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWithFullFramework.DataBases
{
    public class FullFrameworkDbContext: DbContext
    {
        public IDbSet<Student> Students { get; set; }

        public FullFrameworkDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

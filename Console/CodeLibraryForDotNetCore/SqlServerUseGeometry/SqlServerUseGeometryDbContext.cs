using CodeLibraryForDotNetCore.SqlServerUseGeometry.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.SqlServerUseGeometry
{
    public class SqlServerUseGeometryDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public SqlServerUseGeometryDbContext(DbContextOptions<SqlServerUseGeometryDbContext> options) : base(options)
        {

        }
    }
}

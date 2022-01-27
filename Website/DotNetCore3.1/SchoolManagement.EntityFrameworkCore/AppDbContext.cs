using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Core;
using SchoolManagement.Core.Models;
using SchoolManagement.EntityFrameworkCore.Seed;

namespace SchoolManagement.EntityFrameworkCore
{
    public class AppDbContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Scroe> Scroes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().ToTable(nameof(Student), SchoolManagementConsts.SchemaName);
            modelBuilder.Entity<Scroe>().ToTable(nameof(Scroe), SchoolManagementConsts.SchemaName);
            modelBuilder.Entity<Course>().ToTable(nameof(Course), SchoolManagementConsts.SchemaName);
            modelBuilder.Entity<TodoItem>().ToTable(nameof(TodoItem), SchoolManagementConsts.SchemaName);
            modelBuilder.Seed();
        }
    }
}

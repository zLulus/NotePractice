using System;
using System.Collections.Generic;
using System.Text;
using CoreWebsite.EntityFramework.Models;
using CoreWebsite.EntityFramework.Models.EntityRelationTest;
using CoreWebsite.EntityFramework.Models.TreeTest;
using CoreWebsite.EntityFramework.Models.UseGeometry;
using Microsoft.EntityFrameworkCore;

namespace CoreWebsite.EntityFramework
{
    public class WebsiteDbContext : DbContext
    {

        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options):base(options)
        {

        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityComment> ActivityComments { get; set; }
        public DbSet<AdmissionRecord> AdmissionRecords { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentTeacherRelationship> StudentTeacherRelationships { get; set; }
        public DbSet<TreeNode> TreeNodes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Road> Roads { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //导航属性
            modelBuilder.Entity<ActivityComment>()
                .HasOne(p => p.Activity)
                .WithMany(b => b.ActivityComments)
                .HasForeignKey(p => p.ActivityId);
            modelBuilder.Entity<Activity>()
                .HasMany(x => x.ActivityComments)
                .WithOne(x => x.Activity)
                .HasForeignKey(x => x.ActivityId);

            //Student-AdmissionRecord 1:1  设置ForeignKey
            //modelBuilder.Entity<Student>()
            //    .HasOne(p => p.AdmissionRecord)
            //    .WithOne(p => p.Student)
            //    .HasForeignKey<Student>(p => p.AdmissionRecordId);

            //Student-Class n:1
            modelBuilder.Entity<Class>()
                .HasMany(p => p.Students)
                .WithOne(p => p.Class)
                .HasForeignKey(p => p.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //下面写法也可以
            //modelBuilder.Entity<Student>()
            //    .HasOne(p => p.Class)
            //    .WithMany(p=>p.Students)
            //    .HasForeignKey(k => k.ClassId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            //Student-Teacher m:n 
            //通过StudentTeacherRelationship中间表，通过实现两个1:n，实现m:n
            modelBuilder.Entity<StudentTeacherRelationship>()
                .HasOne(p => p.Student)
                .WithMany(p => p.StudentTeacherRelationships)
                .HasForeignKey(k => k.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<StudentTeacherRelationship>()
                .HasOne(p => p.Teacher)
                .WithMany(p => p.StudentTeacherRelationships)
                .HasForeignKey(k => k.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //单表树状结构
            modelBuilder.Entity<TreeNode>()
                //主语this，拥有Children
                .HasMany(x => x.Children)
                //主语Children，每个Child拥有一个Parent
                .WithOne(x => x.Parent)
                //主语Children，每个Child的外键是ParentId
                .HasForeignKey(x => x.ParentId)
                //这里必须是非强制关联，否则报错：Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}

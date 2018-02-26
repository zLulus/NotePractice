using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ContosoUniversity.DAL
{
    public class SchoolContext : DbContext
    {
        //base传参nameOrConnectionString   这样写就要在配置文件里面加一个SchoolContext的数据库连接字符串
        public SchoolContext() : base("SchoolContext")
        {
        }

        //an entity set typically corresponds to a database table, and an entity corresponds to a row in the table
        //DbSet对应一张表，一个实体（Student）对应一行记录
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //去掉将表名称设置为实体类型名称的复数形式的约定
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
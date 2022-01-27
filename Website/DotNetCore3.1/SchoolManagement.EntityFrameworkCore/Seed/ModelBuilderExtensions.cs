using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Core.Models;
using SchoolManagement.Core.Models.Enums;

namespace SchoolManagement.EntityFrameworkCore.Seed
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 2,
                    Name = "张三",
                    Major = MajorEnum.ComputerScience,
                    Email = "zhangsan@qq.com"
                }
            );
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 3,
                    Name = "李四",
                    Major = MajorEnum.Math,
                    Email = "lisi@360.com"
                }
            );
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Title = "数学"
                }
            );
            modelBuilder.Entity<Scroe>().HasData(
                new Scroe
                {
                    Id = 1,
                    CourseId = 1,
                    StudentId = 2,
                    ScroeNumber = 99
                }
            );
            modelBuilder.Entity<Scroe>().HasData(
                new Scroe
                {
                    Id = 2,
                    CourseId = 1,
                    StudentId = 3,
                    ScroeNumber = 90
                }
            );
        }
    }
}

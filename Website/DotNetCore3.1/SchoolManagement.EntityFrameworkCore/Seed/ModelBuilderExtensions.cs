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
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreWebsite.EntityFramework.Models.EntityRelationTest
{
    /// <summary>
    /// 学生-教师关系表
    /// </summary>
    public class StudentTeacherRelationship
    {
        [Key]
        public long Id { get; set; }
        public long StudentId { get; set; }
        public virtual Student Student { get; set; }
        public long TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}

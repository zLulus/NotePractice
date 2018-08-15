using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreWebsite.EntityFramework.Models.EntityRelationTest
{
    /// <summary>
    /// 教师
    /// </summary>
    public class Teacher
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string TeacherId { get; set; }
        public virtual List<StudentTeacherRelationship> StudentTeacherRelationships { get; set; }
    }
}

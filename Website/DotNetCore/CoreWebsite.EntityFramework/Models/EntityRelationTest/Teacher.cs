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
    }
}

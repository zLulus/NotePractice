using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreWebsite.EntityFramework.Models.EntityRelationTest
{
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

using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebsite.EntityFramework.Dtos.EntityRelationTest
{
    public class StudentTeacherRelationshipDto
    {
        public long Id { get; set; }
        public long TeacherId { get; set; }
        public virtual TeacherDto Teacher { get; set; }
    }
}

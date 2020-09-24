using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebsite.EntityFramework.Dtos.EntityRelationTest
{
    public class StudentDto
    {
        public long Id { get; set; }
        public string StudentId { get; set; }
        public string Name { get; set; }
        public long AdmissionRecordId { get; set; }
        public virtual AdmissionRecordDto AdmissionRecord { get; set; }
        public long ClassId { get; set; }
        public virtual ClassDto Class { get; set; }
        public virtual List<StudentTeacherRelationshipDto> StudentTeacherRelationships { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreWebsite.EntityFramework.Models.EntityRelationTest
{
    /// <summary>
    /// 学生
    /// </summary>
    public class Student
    {
        [Key]
        public long Id { get; set; }
        public string StudentId { get; set; }
        public string Name { get; set; }
        public long AdmissionRecordId { get; set; }
        //Student-AdmissionRecord 1:1
        [ForeignKey("AdmissionRecordId")]
        public virtual AdmissionRecord AdmissionRecord { get; set; }
        public long ClassId { get; set; }
        public virtual Class Class { get; set; }
        public virtual List<StudentTeacherRelationship> StudentTeacherRelationships { get; set; }
    }
}

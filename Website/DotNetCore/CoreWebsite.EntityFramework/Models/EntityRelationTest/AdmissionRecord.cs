using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreWebsite.EntityFramework.Models.EntityRelationTest
{
    /// <summary>
    /// 入学记录
    /// </summary>
    public class AdmissionRecord
    {
        [Key]
        public long Id { get; set; }
        public long StudentId { get; set; }
        //Student-AdmissionRecord 1:1
        [ForeignKey("AdmissionRecordId")]
        public virtual Student Student { get;set;}
    }
}

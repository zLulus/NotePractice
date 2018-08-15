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
        public DateTime AdmissionTime { get; set; }
        public string Remark { get; set; }
    }
}

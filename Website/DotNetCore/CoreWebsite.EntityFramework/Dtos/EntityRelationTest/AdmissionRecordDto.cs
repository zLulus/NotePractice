using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebsite.EntityFramework.Dtos.EntityRelationTest
{
    public class AdmissionRecordDto
    {
        public long Id { get; set; }
        public DateTime AdmissionTime { get; set; }
        public string Remark { get; set; }
    }
}

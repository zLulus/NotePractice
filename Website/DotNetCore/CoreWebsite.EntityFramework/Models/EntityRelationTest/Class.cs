using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreWebsite.EntityFramework.Models.EntityRelationTest
{
    /// <summary>
    /// 班级
    /// </summary>
    public class Class
    {
        [Key]
        public long Id { get; set; }
    }
}

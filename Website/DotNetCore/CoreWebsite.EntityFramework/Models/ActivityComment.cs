using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreWebsite.EntityFramework.Models
{
    public class ActivityComment
    {
        [Key]
        public long Id { get; set; }
        public string Content { get; set; }
        public long ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}

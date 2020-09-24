using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreWebsite.EntityFramework.Models
{
    public class Activity
    {
        [Key]
        public long Id { get; set; }
        public virtual List<ActivityComment> ActivityComments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Core.Models
{
    public class Scroe
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int ScroeNumber { get; set; }
    }
}

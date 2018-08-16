using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWithFullFramework.DataBases.Models
{
    public class Student
    { 
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}

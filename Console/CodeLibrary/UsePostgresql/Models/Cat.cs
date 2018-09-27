using CodeLibrary.UsePostgresql.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.UsePostgresql.Models
{
    public class Cat
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public SexEnum Sex { get; set; }
    }
}

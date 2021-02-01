using CodeLibraryForDotNetCore.UsePostgresql.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeLibraryForDotNetCore.UsePostgresql.Models
{
    public class Dog
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public SexEnum Sex { get; set; }
    }
}

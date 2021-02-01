using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeLibraryForDotNetCore.UsePostgresql.Models.UseGeometry
{
    public class Country
    {
        [Key]
        public long Id { get; set; }
        public string CountryName { get; set; }
        public Polygon Border { get; set; }
    }
}

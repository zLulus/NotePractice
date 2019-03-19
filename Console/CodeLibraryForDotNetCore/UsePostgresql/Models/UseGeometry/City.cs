using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeLibraryForDotNetCore.UsePostgresql.Models.UseGeometry
{
    public class City
    {
        [Key]
        public long Id { get; set; }
        public string CityName { get; set; }
        public Point Location { get; set; }
        public long CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
}
}

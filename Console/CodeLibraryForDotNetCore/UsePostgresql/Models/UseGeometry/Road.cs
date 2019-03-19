using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeLibraryForDotNetCore.UsePostgresql.Models.UseGeometry
{
    public class Road
    {
        [Key]
        public long Id { get; set; }
        public string RoadName { get; set; }
        public LineString Line { get; set; }
        public long? CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
}
}

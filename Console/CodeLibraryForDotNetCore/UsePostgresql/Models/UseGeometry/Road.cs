using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeLibraryForDotNetCore.UsePostgresql.Models.UseGeometry
{
    public class Road
    {
        [Key]
        public long Id { get; set; }
        public string RoadName { get; set; }
        public LineString Line { get; set; }
    }
}

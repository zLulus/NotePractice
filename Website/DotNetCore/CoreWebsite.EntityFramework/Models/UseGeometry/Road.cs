using GeoAPI.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreWebsite.EntityFramework.Models.UseGeometry
{
    [Table("Roads")]
    public class Road
    {
        [Key]
        public int RoadID { get; set; }

        public string RoadName { get; set; }

        public ILineString Line { get; set; }
    }
}

using GeoAPI.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreWebsite.EntityFramework.Models.UseGeometry
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        public int CountryID { get; set; }

        public string CountryName { get; set; }

        // Database includes both Polygon and MultiPolygon values
        public IGeometry Border { get; set; }
    }
}

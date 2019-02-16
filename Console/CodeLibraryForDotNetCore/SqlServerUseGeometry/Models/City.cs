using GeoAPI.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeLibraryForDotNetCore.SqlServerUseGeometry.Models
{
    [Table("Cities")]
    public class City
    {
        public int CityID { get; set; }

        public string CityName { get; set; }

        public IPoint Location { get; set; }
    }
}

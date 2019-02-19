using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.EntityFramework;
using CoreWebsite.EntityFramework.Models.UseGeometry;
using GeoAPI.Geometries;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite;

namespace CoreWebsite.Controllers
{
    public class SqlServerUseGeometryController : Controller
    {
        //参考资料
        //https://docs.microsoft.com/zh-cn/ef/core/modeling/spatial
        //https://docs.microsoft.com/zh-cn/sql/t-sql/spatial-geometry/spatial-types-geometry-transact-sql?view=sql-server-2017
        private readonly WebsiteDbContext _dbContext;
        public SqlServerUseGeometryController(WebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult CreatePoint(string cityName,double x, double y)
        {
            IPoint currentLocation = GetLocation(x, y);
            _dbContext.Cities.Add(new City()
            {
                CityName = cityName,
                Location = currentLocation
            });
            _dbContext.SaveChanges();
            return Json("ok");
        }

        private static IPoint GetLocation(double x, double y)
        {
            //SRID:所使用的坐标空间引用系统
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var currentLocation = geometryFactory.CreatePoint(new Coordinate(x, y));
            return currentLocation;
        }

        public IActionResult UpdatePoint(int citiId, string cityName, double x, double y)
        {
            var city = _dbContext.Cities.FirstOrDefault(c => c.CityID == citiId);
            if (city == null)
            {
                return Json($"查询不到id为{citiId}的城市");
            }
            city.CityName = cityName;
            city.Location = GetLocation(x, y);
            return Json("ok");
        }

        public IActionResult GetPoint()
        {
            return Json("ok");
        }

        public IActionResult GetPolygon()
        {
            return Json("ok");
        }
    }
}
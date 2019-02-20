using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.EntityFramework;
using CoreWebsite.EntityFramework.Models.UseGeometry;
using GeoAPI.Geometries;
using GeoJSON.Net.Geometry;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace CoreWebsite.Controllers
{
    public class SqlServerUseGeometryController : Controller
    {
        //参考资料
        //https://docs.microsoft.com/zh-cn/ef/core/modeling/spatial
        //https://docs.microsoft.com/zh-cn/sql/t-sql/spatial-geometry/spatial-types-geometry-transact-sql?view=sql-server-2017
        private readonly WebsiteDbContext _dbContext;
        private static int srid = 4326;
        public SqlServerUseGeometryController(WebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Point
        //http://localhost:61541/SqlServerUseGeometry/CreatePoint?cityName=%E5%8C%97%E4%BA%AC&x=100&y=10
        public IActionResult CreatePoint(string cityName, double x, double y)
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
            //空间引用标识符(spatial reference identifier, SRID):所使用的坐标空间引用系统
            //srid=4326:wgs84
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: srid);
            var currentLocation = geometryFactory.CreatePoint(new Coordinate(x, y));
            return currentLocation;
        }

        //http://localhost:61541/SqlServerUseGeometry/UpdatePoint?citiId=1&cityName=%E6%88%90%E9%83%BD&x=100&y=10
        public IActionResult UpdatePoint(int citiId, string cityName, double x, double y)
        {
            var city = _dbContext.Cities.FirstOrDefault(c => c.CityID == citiId);
            if (city == null)
            {
                return Json($"查询不到id为{citiId}的城市");
            }
            city.CityName = cityName;
            city.Location = GetLocation(x, y);
            _dbContext.SaveChanges();
            return Json("ok");
        }

        //http://localhost:61541/SqlServerUseGeometry/GetPoint/1
        [Route("/SqlServerUseGeometry/GetPoint/{citiId}")]
        public IActionResult GetPoint(int citiId)
        {
            var city = _dbContext.Cities.FirstOrDefault(c => c.CityID == citiId);
            if (city == null)
            {
                return Json($"查询不到id为{citiId}的城市");
            }

            //理解:Geometry/geojson/WKT
            //Geometry to GeoJSON
            //GeoJSON:http://geojson.org/
            //GeoJSON.Net:https://github.com/GeoJSON-Net/GeoJSON.Net
            Position position = new Position(city.Location.X, city.Location.Y);
            GeoJSON.Net.Geometry.Point point = new GeoJSON.Net.Geometry.Point(position);
            var s = JsonConvert.SerializeObject(point);

            //Geometry to wkt
            //var wkt = SharpMap.Converters.WellKnownText.GeometryToWKT.Write(city.Location);
            return Json(s);
        }
        #endregion

        #region Road
        [Route("/SqlServerUseGeometry/CreateRoad/{roadName}")]
        public IActionResult CreateRoad(string roadName)
        {
            //LinearRing的点必须形成一个封闭的线串，而LineString则不需要
            var line = new NetTopologySuite.Geometries.LineString(new Coordinate[]
            {
                new Coordinate(10,0),
                new Coordinate(10,10),
                new Coordinate(0,10),
                new Coordinate(0,0),
                //new Coordinate(10,0),
            });
            //设置坐标系
            line.SRID = srid;
            _dbContext.Roads.Add(new Road()
            {
                RoadName= roadName,
                Line = line
            });
            _dbContext.SaveChanges();
            return Json("ok");
        }

        public IActionResult GetRoad(int roadId)
        {
            return Json("ok");
        }
        #endregion

        #region Polygon
        //http://localhost:61541/SqlServerUseGeometry/CreatePolygon/%E4%B8%AD%E5%9B%BD
        [Route("/SqlServerUseGeometry/CreatePolygon/{countryName}")]
        public IActionResult CreatePolygon(string countryName)
        {
            var geom =
             new NetTopologySuite.Geometries.Polygon(
                new LinearRing(new Coordinate[]
                {
                    //逆时针绘制
                    new Coordinate(10,0),
                    new Coordinate(10,10),
                    new Coordinate(0,10),
                    new Coordinate(0,0),
                    new Coordinate(10,0),
                }));
            //设置坐标系
            geom.SRID = srid;
            _dbContext.Countries.Add(new Country()
            {
                CountryName = countryName,
                Border = geom
            });
            _dbContext.SaveChanges();
            return Json("ok");
        }

        public IActionResult GetPolygon()
        {
            return Json("ok");
        }
        #endregion
    }
}
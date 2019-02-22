using CodeLibraryForDotNetCore.UsePostgresql.Models.UseGeometry;
using GeoAPI.Geometries;
using GeoJSON.Net.Geometry;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibraryForDotNetCore.UsePostgresql
{
    public class PostgresqlUseGeometryDemo
    {
        private readonly TestDbContext _dbContext;
        private readonly int srid= 4326;
        public PostgresqlUseGeometryDemo(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Run()
        {
            await AddTestData();
            await GetTestData();
        }

        private async Task GetTestData()
        {
            await GetPointData();
            await GetLineStringData();
            await GetPolygonData();
            CalculateDistanceBetweenPoints(0, 0, 10, 10);
            CheckIsPointInRegion(1.5, 1.3);
            CheckIsPointInRegion(-10, -10);
            throw new NotImplementedException();
        }

        private async Task GetPointData()
        {
            var city = await _dbContext.Cities.FirstOrDefaultAsync();
            //Geometry to geoJSON
            Position position = new Position(city.Location.X, city.Location.Y);
            GeoJSON.Net.Geometry.Point point = new GeoJSON.Net.Geometry.Point(position);
            var s = JsonConvert.SerializeObject(point);
            //Geometry to wkt
            var s2 = NetTopologySuite.IO.WKTWriter.ToPoint(city.Location.Coordinate);
            Console.WriteLine($"GeoJSON结果:{s},WKT结果:{s2}");
        }
        
        private async Task GetLineStringData()
        {
            var road = await _dbContext.Roads.FirstOrDefaultAsync();
            //Geometry to geoJSON
            var coordinates = new List<IPosition>();
            foreach (var item in road.Line.Coordinates)
            {
                coordinates.Add(new Position(item.X, item.Y, item.Z));
            }
            GeoJSON.Net.Geometry.LineString line = new GeoJSON.Net.Geometry.LineString(coordinates);
            var s = JsonConvert.SerializeObject(line);
            //Geometry to wkt
            var s2 = NetTopologySuite.IO.WKTWriter.ToLineString(road.Line.CoordinateSequence);
            Console.WriteLine($"GeoJSON结果:{s},WKT结果:{s2}");
        }

        private async Task GetPolygonData()
        {
            var country = await _dbContext.Countries.FirstOrDefaultAsync();
            //Geometry to geoJSON
            var lines = new List<GeoJSON.Net.Geometry.LineString>();
            var polygon = country.Border as NetTopologySuite.Geometries.Polygon;
            List<Coordinate[]> res = new List<Coordinate[]>();
            res.Add(polygon.Shell.Coordinates);
            foreach (ILineString interiorRing in polygon.InteriorRings)
            {
                res.Add(interiorRing.Coordinates);
            }
            foreach (var line in res)
            {
                var coordinates = new List<IPosition>();
                foreach (var item in line)
                {
                    coordinates.Add(new Position(item.X, item.Y, item.Z));
                }
                lines.Add(new GeoJSON.Net.Geometry.LineString(coordinates));
            }
            GeoJSON.Net.Geometry.Polygon jsonPolygon = new GeoJSON.Net.Geometry.Polygon(lines);
            var s = JsonConvert.SerializeObject(jsonPolygon);
            //Geometry to wkt
            var writer = new NetTopologySuite.IO.WKTWriter();
            var s2 = writer.Write(country.Border);
            Console.WriteLine($"GeoJSON结果:{s},WKT结果:{s2}");
        }

        private async Task AddTestData()
        {
            var point = new NetTopologySuite.Geometries.Point(new Coordinate(10, 10));
            point.SRID = srid;
            _dbContext.Cities.Add(new City()
            {
                CityName="ChengDu",
                Location= point
            });
            var line = new NetTopologySuite.Geometries.LineString(new Coordinate[]
            {
                new Coordinate(0,0),
                new Coordinate(10,0),
                new Coordinate(10,10),
                new Coordinate(0,10)
            });
            line.SRID = srid;
            _dbContext.Roads.Add(new Road()
            {
                RoadName="Road Name 1",
                Line= line
            });
            var border = new NetTopologySuite.Geometries.Polygon(new LinearRing(new Coordinate[]
            {
                new Coordinate(0,0),
                new Coordinate(10,0),
                new Coordinate(10,10),
                new Coordinate(0,10),
                new Coordinate(0,0)
            }));
            border.SRID = srid;
            _dbContext.Countries.Add(new Country()
            {
                CountryName = "China",
                Border = border
            });
            await _dbContext.SaveChangesAsync();
        }

        #region Calculation
        public void CalculateDistanceBetweenPoints(double x1, double y1, double x2, double y2)
        {
            //https://nettopologysuite.github.io/html/class_net_topology_suite_1_1_operation_1_1_distance_1_1_distance_op.html
            var point1 = new NetTopologySuite.Geometries.Point(x1, y1) { SRID = srid };
            var point2 = new NetTopologySuite.Geometries.Point(x2, y2) { SRID = srid };
            var distance = NetTopologySuite.Operation.Distance.DistanceOp.Distance(point1, point2);
            Console.WriteLine($"点({x1},{y1})和点({x2},{y2})的距离是{distance}.");
        }
        
        public void CheckIsPointInRegion(double x, double y)
        {
            var geom = new NetTopologySuite.Geometries.Polygon(
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
            var point = new NetTopologySuite.Geometries.Point(x, y) { SRID = srid };
            //https://stackoverflow.com/questions/53820355/fast-find-if-points-belong-to-polygon-nettopologysuite-geometries-c-net-cor
            var prepGeom = NetTopologySuite.Geometries.Prepared.PreparedGeometryFactory.Prepare(geom);
            var isContain = prepGeom.Contains(point);
            Console.WriteLine(isContain ? $"点({x},{y})包含在面以内" : $"点({x},{y})不包含在面以内");
        }
        #endregion
    }
}

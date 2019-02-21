using CodeLibraryForDotNetCore.UsePostgresql.Models.UseGeometry;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
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
            #region Point
            var city = await _dbContext.Cities.FirstOrDefaultAsync();
            //Geometry to geoJSON
            //Geometry to wkt
            Console.WriteLine("");
            #endregion
            #region LineString
            var road = await _dbContext.Roads.FirstOrDefaultAsync();
            //Geometry to geoJSON
            //Geometry to wkt
            Console.WriteLine("");
            #endregion
            #region Polygon
            var coutry = await _dbContext.Countries.FirstOrDefaultAsync();
            //Geometry to geoJSON
            //Geometry to wkt
            Console.WriteLine("");
            #endregion
            throw new NotImplementedException();
        }

        private async Task AddTestData()
        {
            _dbContext.Cities.Add(new City()
            {
                CityName="ChengDu",
                Location=new Point(new Coordinate(10,10))
            });
            _dbContext.Roads.Add(new Road()
            {
                RoadName="Road Name 1",
                Line=new LineString(new Coordinate[]
                {
                    new Coordinate(0,0),
                    new Coordinate(10,0),
                    new Coordinate(10,10),
                    new Coordinate(0,10)
                })
            });
            _dbContext.Countries.Add(new Country()
            {
                CountryName = "China",
                Border = new Polygon(new LinearRing(new Coordinate[]
                {
                    new Coordinate(0,0),
                    new Coordinate(10,0),
                    new Coordinate(10,10),
                    new Coordinate(0,10),
                    new Coordinate(0,0)
                }))
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}

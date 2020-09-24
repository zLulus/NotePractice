using CodeLibraryForDotNetCore.LinqTools.ViewModels;
using CodeLibraryForDotNetCore.UsePostgresql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeLibraryForDotNetCore.LinqTools
{
    public class LinqToolDemo
    {
        private readonly TestDbContext _dbContext;
        public LinqToolDemo(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Run()
        {
            //联表查询符合条件的Road数据
            var filter = "Road";
            var query = from road in _dbContext.Roads
                        join city in _dbContext.Cities on road.CityId equals city.Id
                        join coutry in _dbContext.Countries on city.CountryId equals coutry.Id
                        select new LocationViewModel
                        {
                            RoadId=road.Id,
                            CountryName=coutry.CountryName,
                            CityName=city.CityName,
                            RoadName=road.RoadName,
                        };
            var entities1 = query.Where(x=>x.RoadName.Contains(filter)).ToList();
            var entities2 = query.Where(x => x.RoadName.Contains(filter)).DistinctBy(x => x.CountryName).ToList();
        }
    }
}

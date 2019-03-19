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
            //var filter = "";
            //var query=from city in _dbContext.Cities
            //          join coutry in _dbContext.Countries on city.
            //var entities= _dbContext.Dogs.DistinctBy(x => x.Id).ToList();
        }
    }
}

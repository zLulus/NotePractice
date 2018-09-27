using CodeLibraryForDotNetCore.UsePostgresql.Enums;
using CodeLibraryForDotNetCore.UsePostgresql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibraryForDotNetCore.UsePostgresql
{
    public class UsePostgresqlDemo
    {
        private readonly TestDbContext _dbContext;
        public UsePostgresqlDemo(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Run()
        {
            _dbContext.Dogs.Add(new Dog() { Name = "Lulu", Sex = SexEnum.Female });
            await _dbContext.SaveChangesAsync();
            var dogs = _dbContext.Dogs.ToList();
        }
    }
}

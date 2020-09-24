using CodeLibrary.UsePostgresql.Enums;
using CodeLibrary.UsePostgresql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.UsePostgresql
{
    public class UsePostgresqlDemo
    {
        public static async Task PostgresqlTest()
        {
            using (TestDbContext dbContext = new TestDbContext())
            {
                dbContext.Cats.Add(new Cat() { Name = "Lulu", Sex = SexEnum.Female });
                await dbContext.SaveChangesAsync();
                var cats = dbContext.Cats.ToList();
            }
        }
    }
}

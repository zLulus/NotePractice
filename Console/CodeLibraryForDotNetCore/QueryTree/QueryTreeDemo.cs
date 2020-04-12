using CodeLibraryForDotNetCore.QueryTree.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeLibraryForDotNetCore.QueryTree
{
    public static class QueryTreeDemo
    {
        private static string connectionString = @"Data Source=D:\MicroDesktop\NotePractice\Console\CodeLibraryForDotNetCore\QueryTree\Db\regionTreeDb.db";
        public static void Run()
        {
            //ef core sqlite 使用
            using(RegionTreeDbContext db=new RegionTreeDbContext(connectionString))
            {
                var region = db.Regions.FirstOrDefault();
            }

            //根据几个节点id，查询对应部分树

        }
    }
}

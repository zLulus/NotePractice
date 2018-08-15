using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreWebsite.Controllers
{
    public class TreeTestController : Controller
    {
        private readonly WebsiteDbContext _dbContext;
        public TreeTestController(WebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult GetAllNodes()
        {
            var node = _dbContext.TreeNodes
                //命名空间：Microsoft.EntityFrameworkCore
                //不写则查询不到导航属性
                .Include(x=>x.Children)
                .FirstOrDefault(x => x.ParentId == null);
            return Json(node);
        }
    }
}
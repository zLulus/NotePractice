using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreWebsite.EntityFramework;
using CoreWebsite.EntityFramework.Dtos.TreeTest;
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
            //因为是递归查询，无法使用Include，需要启动延迟加载
            //https://docs.microsoft.com/zh-cn/ef/core/querying/related-data#lazy-loading
            var node = _dbContext.TreeNodes
                //这里默认只有一个根节点，ParentId=null
                .FirstOrDefault(x => x.ParentId == null);
            var dto = Mapper.Map<TreeNodeDto>(node);
            return Json(dto);
        }
    }
}
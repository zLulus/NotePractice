using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.EntityFramework;
using CoreWebsite.EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreWebsite.Controllers
{
    public class DbTestController : Controller
    {
        private readonly WebsiteDbContext _dbContext;
        public DbTestController(WebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult TestInclude()
        {
            var activitiesWithComments= _dbContext.Activities.Include(x => x.ActivityComments).ToList();
            return Json(activitiesWithComments);
        }

        public IActionResult TestAttchToUpdate()
        {
            //attch:
            //如果没有id,attach的默认行为是新增
            //如果有id,attach的默认行为是修改
            var updateAcitivity = new Activity()
            {
                Id = 1,
                ActivityComments = new List<ActivityComment>()
            };
            updateAcitivity.ActivityComments.Add(new ActivityComment()
            {
                ActivityId = 1,
                Content = "这是更新",
                Id = 1
            });
            updateAcitivity.ActivityComments.Add(new ActivityComment()
            {
                ActivityId = 1,
                Content = "这是新增",
            });
            //删除Id=2的Comment

            _dbContext.Activities.Attach(updateAcitivity);
            
            return Json("");
        }

        public IActionResult TestIncludeAndSelect()
        {
            var result= _dbContext.Activities.Where(x => x.Id == 3).Include(x => x.ActivityComments)
                .Select(x => x.ActivityComments);
            return Json(result);
        }
    }
}
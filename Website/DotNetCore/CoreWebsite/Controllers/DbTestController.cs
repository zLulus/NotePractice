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

        public IActionResult TestAttchTpUpdate()
        {
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
    }
}
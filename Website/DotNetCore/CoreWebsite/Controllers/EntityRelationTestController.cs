using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Controllers
{
    public class EntityRelationTestController : Controller
    {
        private readonly WebsiteDbContext _dbContext;
        public EntityRelationTestController(WebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult GetStudentList()
        {
            var students = _dbContext.Students.ToList();
            return Json(students);
        }
    }
}
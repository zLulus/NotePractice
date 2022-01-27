using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Dtos;
using SchoolManagement.Application.Students;
using SchoolManagement.Core.Models;
using SchoolManagement.Core.Repositories;

namespace SchoolManagement.Mvc.Controllers
{
    //使用service
    [Route("api/[controller]")]
    [ApiController]
    public class SearchStudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public SearchStudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> SearchStudent(string searchString,int currentPage)
        {
            return new JsonResult(new PaginationDto<Student>()
            {
                Count = await _studentService.GetCount(searchString),
                CurrentPage = currentPage,
                Data = await _studentService.GetPaginatedResult(searchString, currentPage)
            });
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core;
using SchoolManagement.Core.Models;
using SchoolManagement.Core.Repositories;

namespace SchoolManagement.Mvc.Controllers
{
    //使用sql
    [Route("api/[controller]")]
    [ApiController]
    public class SearchStudentBySqlController : ControllerBase
    {
        private readonly IRepository<Student, int> _studentRepository;
        public SearchStudentBySqlController(IRepository<Student, int> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SearchStudentBySql(int id)
        {
            string sql = $"SELECT * FROM {SchoolManagementConsts.SchemaName}.{nameof(Student)} WHERE  {nameof(Student.Id)} ={id}";
            return new JsonResult(_studentRepository.FromSqlRaw(sql));
        }
    }
}

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
    public class SearchStudentBySql2Controller : ControllerBase
    {
        private readonly IRepository<Student, int> _studentRepository;
        public SearchStudentBySql2Controller(IRepository<Student, int> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SearchStudentBySql(int id)
        {
            string sql = $"SELECT * FROM {SchoolManagementConsts.SchemaName}.{nameof(Student)} WHERE  {nameof(Student.Id)} ={id}";
            var data = await _studentRepository.ExecuteReaderAsync(sql);
            return new JsonResult(data);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Models;
using SchoolManagement.Core.Repositories;
using System.Threading.Tasks;
using SchoolManagement.Core;

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

        [HttpGet("GetScore/{courseId}/{studentId}")]
        public async Task<IActionResult> GetScoreBySql(int courseId, int studentId)
        {
            string sql = $"select sc.{nameof(Scroe.ScroeNumber)}" + "\n" +
                $"from {SchoolManagementConsts.SchemaName}.{nameof(Course)} c" + "\n" +
                $"join {SchoolManagementConsts.SchemaName}.{nameof(Scroe)} sc on c.{nameof(Course.Id)} = sc.{nameof(Scroe.CourseId)}" + "\n" +
                $"join {SchoolManagementConsts.SchemaName}.{nameof(Student)} s on s.{nameof(Scroe.Id)} = sc.{nameof(Scroe.StudentId)}" + "\n" +
                $"where c.{nameof(Course.Id)} = {courseId}" + "\n" +
                $"and s.{nameof(Student.Id)} = {studentId}";
            var data = await _studentRepository.ExecuteReaderAsync(sql);
            return new JsonResult(data);
        }

         [HttpGet("GetStudentById/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            string sql = $"SELECT * FROM {SchoolManagementConsts.SchemaName}.{nameof(Student)} WHERE  {nameof(Student.Id)} ={id}";
            var data = await _studentRepository.ExecuteReaderAsync(sql);
            return new JsonResult(data);
        }
    }
}

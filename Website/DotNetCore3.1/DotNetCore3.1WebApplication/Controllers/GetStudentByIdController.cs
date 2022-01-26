using System.Reflection.Metadata;
using DotNetCore3._1WebApplication.Common;
using DotNetCore3._1WebApplication.Service;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore3._1WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetStudentByIdController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// 使用构造函数注入的方式注入IStudentRepository
        /// </summary>
        /// <param name="studentRepository"></param>
        public GetStudentByIdController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("{id}")]
        public ObjectResult Get(int id)
        {
            var student = _studentRepository.GetStudent(id);
            if (student == null)
                throw new CustomException($"查询不到id为{id}的学生信息");
            return new ObjectResult(student);
        }
    }
}

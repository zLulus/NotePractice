using AutoMapper;
using DotNet6WebAPI.Domain.Students.Entities;
using DotNet6WebAPI.Dtos.Students;
using Microsoft.AspNetCore.Mvc;

namespace DotNet6WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly Student _student;
        private readonly IMapper _mapper;

        public StudentController(Student student,
            IMapper mapper)
        {
            _student = student;
            _mapper = mapper;
        }

        /// <summary>
        /// Search student information by id(根据id查询学生信息)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<StudentDto> Get(string id)
        {
            var entity = await _student.Get(id);
            return _mapper.Map<StudentDto>(entity);
        }
    }
}

using AutoMapper;
using DotNet6WebAPI.Domain.Students.Entities;
using DotNet6WebAPI.Dtos.Students;
using Microsoft.AspNetCore.Mvc;

namespace DotNet6WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly Student _student;
        private readonly IMapper _mapper;

        public StudentController(Student student,
            IMapper mapper)
        {
            _student = student;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<StudentDto> Get(string id)
        {
            var entity = await _student.Get(id);
            return _mapper.Map<StudentDto>(entity);
        }
    }
}

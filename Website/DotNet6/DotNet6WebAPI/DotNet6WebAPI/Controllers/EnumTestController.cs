using DotNet6WebAPI.Domain.Enums;
using DotNet6WebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DotNet6WebAPI.Controllers
{
    public class EnumTestController : ControllerBase
    {
        [HttpGet(nameof(GetEnum))]
        public ActionResult<TeacherDto> GetEnum()
        {
            return Ok(new TeacherDto()
            {
                Id = Guid.NewGuid().ToString(),
                Age = 23,
                Name = "teacher 1",
                TeacherLevel = TeacherLevel.Assistant
            });
        }

        [HttpPost(nameof(PostEnum))]
        public IActionResult PostEnum([FromBody] TeacherDto teacherDto)
        {
            return Ok($"The value of TeacherLevel is {teacherDto.TeacherLevel}");
        }
    }
}

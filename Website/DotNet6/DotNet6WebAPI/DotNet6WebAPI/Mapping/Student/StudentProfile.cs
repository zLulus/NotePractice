using AutoMapper;
using DotNet6WebAPI.Dtos.Students;

namespace DotNet6WebAPI.Mapping.Student
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDto, DotNet6WebAPI.Domain.Students.Entities.Student>().ReverseMap();
        }
    }
}

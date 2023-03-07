using AutoMapper;
using DotNet6WebAPI.Dtos;

namespace DotNet6WebAPI.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDto, Domain.Entities.Student>().ReverseMap();
        }
    }
}

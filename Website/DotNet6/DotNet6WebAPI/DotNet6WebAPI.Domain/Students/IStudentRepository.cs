using DotNet6WebAPI.Domain.Students.Entities;

namespace DotNet6WebAPI.Domain.Students
{
    public interface IStudentRepository
    {
        Task<Student> Get(string id);
    }
}

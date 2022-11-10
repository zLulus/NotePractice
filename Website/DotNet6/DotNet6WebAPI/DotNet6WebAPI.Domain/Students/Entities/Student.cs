using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet6WebAPI.Domain.Students.Entities
{
    [Table("students")]
    public class Student
    {
        [Description("id")]
        public string Id { get; set; }
        [Description("name")]
        public string Name { get; set; }
        [Description("age")]
        public int Age { get; set; }
        [Description("phone_number")]
        public string PhoneNumber { get; set; }

        private readonly IStudentRepository _studentRepository;

        public Student()
        {

        }

        public Student(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> Get(string id)
        {
            return await _studentRepository.Get(id);
        }
    }
}
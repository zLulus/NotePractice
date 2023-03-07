using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet6WebAPI.Domain.Entities
{
    /// <summary>
    /// student(学生)
    /// </summary>
    [Table("students")]
    public class Student
    {
        /// <summary>
        /// id
        /// </summary>
        [Description("id")]
        public string Id { get; set; }
        /// <summary>
        /// name(名称)
        /// </summary>
        [Description("name")]
        public string Name { get; set; }
        /// <summary>
        /// age(年龄)
        /// </summary>
        [Description("age")]
        public int Age { get; set; }
        /// <summary>
        /// phone number(电话号码)
        /// </summary>
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
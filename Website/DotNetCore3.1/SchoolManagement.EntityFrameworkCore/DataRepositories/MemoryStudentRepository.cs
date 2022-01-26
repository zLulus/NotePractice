using SchoolManagement.Core.Models;
using SchoolManagement.Core.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.EntityFrameworkCore.DataRepositories
{
    public class MemoryStudentRepository : IStudentRepository
    {
        private List<Student> _studentList;
        public MemoryStudentRepository()
        {
            _studentList = new List<Student>()
            {
                new Student() {Id = 1,Name = "张三",Major =MajorEnum.ComputerScience, Email = "zhangsan@qq.com" },
                new Student() {Id = 2,Name = "李四",Major = MajorEnum.Math, Email = "lisi@qq.com" },
                new Student() {Id = 3,Name = "赵六",Major = MajorEnum.English, Email = "zhaoliu@qq.com" },
            };
        }
        public Student GetStudent(int id)
        {
            return _studentList.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentList;
        }
    }
}

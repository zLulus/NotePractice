using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCore3._1WebApplication.Model;

namespace DotNetCore3._1WebApplication.Service
{
    public class MemoryStudentRepository: IStudentRepository
    {
        private List<Student> _studentList;
        public MemoryStudentRepository()
        {
            _studentList = new List<Student>()
            {
                new Student() {Id = 1,Name = "张三",Major = "计算机科学",Email =
                    "zhangsan@qq.com" },
                new Student() {Id = 2,Name = "李四",Major = "物流",Email =
                    "lisi@qq.com" },
                new Student() {Id = 3,Name = "赵六",Major = "电子商务",Email =
                    "zhaoliu@qq.com" },
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

using BlazorDemoApp.Models;

namespace BlazorDemoApp.Services
{
    public class StudentService
    {
        private static List<Student> students = new List<Student>
    {
        new Student { Id = 1, Name = "张三", Email = "zhangsan@example.com", Age = 20 },
        new Student { Id = 2, Name = "李四", Email = "lisi@example.com", Age = 21 },
        new Student { Id = 3, Name = "王五", Email = "wangwu@example.com", Age = 19 }  
        // 添加更多学生数据...  
    };

        public Task<List<Student>> GetStudentsAsync()
        {
            return Task.FromResult(students);
        }
    }
}

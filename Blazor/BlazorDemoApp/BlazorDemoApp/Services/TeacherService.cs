using BlazorDemoApp.Models;

namespace BlazorDemoApp.Services
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetTeachersAsync();
    }

    public class TeacherService: ITeacherService
    {
        private static List<Teacher> students = new List<Teacher>
        {
            new Teacher (1, "张三", new DateOnly(1985, 3, 16)),
            new Teacher (2, "李四",new DateOnly(1985, 3, 16)),
            new Teacher (3,"王五",  new DateOnly(1985, 3, 16))
        };

        public Task<List<Teacher>> GetTeachersAsync()
        {
            return Task.FromResult(students);
        }
    }
}

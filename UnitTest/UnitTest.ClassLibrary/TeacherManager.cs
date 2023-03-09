using UnitTest.ClassLibrary.Entities;

namespace UnitTest.ClassLibrary
{
    public class TeacherManager
    {
        List<Teacher> _memoryTeachers;

        public TeacherManager()
        {
            _memoryTeachers = new List<Teacher>();
        }

        public virtual Teacher Insert(Teacher teacher)
        {
            if (teacher == null)
                return null;
            teacher.Id = Guid.NewGuid();
            _memoryTeachers.Add(teacher);
            return teacher;
        }
    }
}

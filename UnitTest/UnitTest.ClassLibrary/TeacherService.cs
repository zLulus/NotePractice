using UnitTest.ClassLibrary.Entities;

namespace UnitTest.ClassLibrary
{
    public class TeacherService
    {
        TeacherManager _teacherManager;
        public TeacherService(TeacherManager teacherManager)
        {
            _teacherManager = teacherManager;
        }

        public Teacher Insert(Teacher teacher)
        {
            if (teacher == null)
            {
                throw new DataNotExistException($"Data cannot be empty.");
            }
            if (string.IsNullOrEmpty(teacher.Name) || string.IsNullOrWhiteSpace(teacher.Name))
            {
                throw new NameIsEmptyOrWhiteSpaceException($"Name cannot be empty.");
            }
            if (teacher.Age < 0)
            {
                throw new AgeIsInvalidException($"Age must be greater than 0.");
            }
            return _teacherManager.Insert(teacher);
        }
    }
}

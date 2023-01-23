using UnitTest.ClassLibrary.Entities;

namespace UnitTest.ClassLibrary
{
    public class StudentService
    {
        List<Student> _memoryStudents;
        public StudentService()
        {
            _memoryStudents = new List<Student>();
        }

        public Student Insert(Student student)
        {
            student.Id = Guid.NewGuid();
            _memoryStudents.Add(student);
            return student;
        }

        public bool Update(Student student)
        {
            var data = Get(student.Id);
            data.Name = student.Name;
            return true;
        }

        public bool Delete(Guid id)
        {
            var data = Get(id);
            _memoryStudents.Remove(data);
            return true;
        }

        public Student Get(Guid id)
        {
            var data = _memoryStudents.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                throw new Exception($"No data with id {id} exists");
            }
            return data;
        }
    }
}

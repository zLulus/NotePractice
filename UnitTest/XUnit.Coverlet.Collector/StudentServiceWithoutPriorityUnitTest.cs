using Shouldly;
using UnitTest.ClassLibrary;
using UnitTest.ClassLibrary.Entities;

namespace XUnit.Coverlet.Collector
{
    public class StudentServiceWithoutPriorityUnitTest
    {
        StudentService _studentService;
        public StudentServiceWithoutPriorityUnitTest()
        {
            _studentService = new StudentService();
        }

        [Fact]
        public Student Insert_A_Student()
        {
            var data = new Student()
            {
                Name = "test name"
            };
            _studentService.Insert(data);
            data.ShouldNotBeNull();
            return data;
        }

        [Fact]
        public void Update_A_Student()
        {
            var data = Insert_A_Student();
            data.Name = "update student name";
            _studentService.Update(data).ShouldBeTrue();
        }

        [Fact]
        public void Update_A_Student_Failed()
        {
            var data = new Student()
            {
                Id = Guid.NewGuid(),
                Name = "test name"
            };
            Should.Throw<DataNotExistException>(() => _studentService.Update(data));
        }


        [Fact]
        public void Get_A_Student()
        {
            var data = Insert_A_Student();
            var getData = _studentService.Get(data.Id);
            getData.ShouldNotBeNull();
            getData.Id.ShouldBeEquivalentTo(data.Id);
        }

        [Fact]
        public void Get_A_Student_Failed()
        {
            Should.Throw<DataNotExistException>(() => _studentService.Get(Guid.NewGuid()));
        }

        [Fact]
        public void Delete_A_Student()
        {
            var data = Insert_A_Student();
            _studentService.Delete(data.Id);
        }

        [Fact]
        public void Delete_A_Student_Failed()
        {
            Should.Throw<DataNotExistException>(() => _studentService.Delete(Guid.NewGuid()));
        }
    }
}

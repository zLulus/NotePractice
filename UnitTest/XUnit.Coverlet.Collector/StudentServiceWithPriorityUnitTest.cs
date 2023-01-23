using Shouldly;
using UnitTest.ClassLibrary;
using UnitTest.ClassLibrary.Entities;
using XUnit.Coverlet.Collector.TestPriority;

namespace XUnit.Coverlet.Collector
{
    [TestCaseOrderer("XUnit.Coverlet.Collector.TestPriority.PriorityOrderer", "XUnit.Coverlet.Collector")]
    public class StudentServiceWithPriorityUnitTest
    {
        static StudentService _studentService;
        static Student data = new Student()
        {
            Name = "test name"
        };

        static StudentServiceWithPriorityUnitTest()
        {
            _studentService = new StudentService();
        }

        [Fact, TestPriority(1)]
        public Student Insert_A_Student()
        {
            _studentService.Insert(data);
            data.ShouldNotBeNull();
            return data;
        }

        [Fact, TestPriority(2)]
        public void Update_A_Student()
        {
            data.Name = "update student name";
            _studentService.Update(data).ShouldBeTrue();
        }

        [Fact, TestPriority(3)]
        public void Update_A_Student_Failed()
        {
            var data = new Student()
            {
                Id = Guid.NewGuid(),
                Name = "test name"
            };
            Should.Throw<Exception>(() => _studentService.Update(data));
        }

        [Fact, TestPriority(4)]
        public void Get_A_Student()
        {
            var getData = _studentService.Get(data.Id);
            getData.ShouldNotBeNull();
            getData.Id.ShouldBeEquivalentTo(data.Id);
        }

        [Fact, TestPriority(5)]
        public void Get_A_Student_Failed()
        {
            Should.Throw<Exception>(() => _studentService.Get(Guid.NewGuid()));
        }

        [Fact, TestPriority(6)]
        public void Delete_A_Student()
        {
            _studentService.Delete(data.Id);
        }

        [Fact, TestPriority(7)]
        public void Delete_A_Student_Failed()
        {
            Should.Throw<Exception>(() => _studentService.Delete(Guid.NewGuid()));
        }
    }
}

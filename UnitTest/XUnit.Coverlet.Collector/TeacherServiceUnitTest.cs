using NSubstitute;
using Shouldly;
using UnitTest.ClassLibrary;
using UnitTest.ClassLibrary.Entities;

namespace XUnit.Coverlet.Collector
{
    public class TeacherServiceUnitTest
    {
        public TeacherService _teacherService;
        public TeacherServiceUnitTest()
        {
            var teacherManager = Substitute.For<TeacherManager>();
            teacherManager.Insert(new Teacher());
            _teacherService = new TeacherService(teacherManager);
        }

        [Fact]
        public void Insert_A_Teacher()
        {
            var teacher = new Teacher()
            {
                Name = "test teacher",
                Age = 30
            };
            _teacherService.Insert(teacher);
            teacher.ShouldNotBeNull();
        }

        [Fact]
        public void Insert_A_Teacher_Failed_Empty_Name()
        {
            var teacher = new Teacher()
            {
                Name = "",
                Age = 30
            };
            Should.Throw<NameIsEmptyOrWhiteSpaceException>(() => _teacherService.Insert(teacher));
        }

        [Fact]
        public void Insert_A_Teacher_Failed_WhiteSpace_Name()
        {
            var teacher = new Teacher()
            {
                Name = "   ",
                Age = 30
            };
            Should.Throw<NameIsEmptyOrWhiteSpaceException>(() => _teacherService.Insert(teacher));
        }

        [Fact]
        public void Insert_A_Teacher_Failed_Without_Data()
        {
            Should.Throw<DataNotExistException>(() => _teacherService.Insert(null));
        }

        [Fact]
        public void Insert_A_Teacher_Failed_Invalid_Age()
        {
            var teacher = new Teacher()
            {
                Name = "test teacher",
                Age = -10
            };
            Should.Throw<AgeIsInvalidException>(() => _teacherService.Insert(teacher));
        }
    }
}

using NSubstitute;
using Shouldly;
using UnitTest.ClassLibrary;
using UnitTest.ClassLibrary.Entities;

namespace XUnit.Coverlet.Collector
{
    public class TeacherServiceMockWithFunctionUnitTest
    {
        public TeacherService _teacherService;
        TeacherManager teacherManager;
        public TeacherServiceMockWithFunctionUnitTest()
        {
            teacherManager = Substitute.For<TeacherManager>();
            //mock Insert method with conditions
            teacherManager
                 .Insert(Arg.Any<Teacher>())
                 .Returns(parameters =>
                 {
                     var teacher = parameters[0] as Teacher;
                     if (teacher == null)
                         return null;
                     if (string.IsNullOrEmpty(teacher.Name))
                         return null;
                     return teacher;
                 });
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

            var returnTeacher = _teacherService.Insert(teacher);
            returnTeacher.ShouldNotBeNull();
        }

        [Fact]
        public void Insert_A_Teacher_WithoutName()
        {
            var teacher = new Teacher()
            {
                Name = "",
                Age = 30
            };
            var returnTeacher = _teacherService.Insert(teacher);
            returnTeacher.ShouldBeNull();
        }

        [Fact]
        public void Insert_A_Teacher_WithoutData()
        {
            var returnTeacher = _teacherService.Insert(null);
            returnTeacher.ShouldBeNull();
        }
    }
}

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
                     if (teacher.Name == "test teacher")
                         return null;
                     if (teacher.Age > 100)
                         return null;
                     return teacher;
                 });
            _teacherService = new TeacherService(teacherManager);
        }

        [Fact]
        public void Insert_A_Teacher_Judge_Name_Return_Null()
        {
            var teacher = new Teacher()
            {
                Name = "test teacher",
                Age = 30
            };

            var returnTeacher = _teacherService.Insert(teacher);
            returnTeacher.ShouldBeNull();
        }

        [Fact]
        public void Insert_A_Teacher_Judge_Age_Return_Null()
        {
            var teacher = new Teacher()
            {
                Name = "test teacher 2",
                Age = 130
            };
            var returnTeacher = _teacherService.Insert(teacher);
            returnTeacher.ShouldBeNull();
        }

        [Fact]
        public void Insert_A_Teacher()
        {
            var teacher = new Teacher()
            {
                Name = "test teacher 3",
                Age = 30
            };

            var returnTeacher = _teacherService.Insert(teacher);
            returnTeacher.ShouldNotBeNull();
        }

    }
}

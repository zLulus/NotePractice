using Shouldly;
using UnitTest.ClassLibrary;
using UnitTest.ClassLibrary.Entities;

namespace XUnit.Coverlet.Collector
{
    public class TeacherManagerUnitTest
    {
        public TeacherManager _teacherManager;
        public TeacherManagerUnitTest()
        {
            _teacherManager = new TeacherManager();
        }

        [Fact]
        public void Insert_A_Teacher()
        {
            var teacher = new Teacher()
            {
                Name = "test teacher",
                Age = 30
            };
            _teacherManager.Insert(teacher);
        }

        [Fact]
        public void Insert_A_Null_Teacher()
        {
            _teacherManager.Insert(null).ShouldBeNull();
        }
    }
}

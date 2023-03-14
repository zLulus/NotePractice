using Shouldly;
using UnitTest.ClassLibrary;

namespace XUnit.Coverlet.Collector
{
    public class InternalTestServiceUnitTest
    {
        [Fact]
        public void Add()
        {
            InternalTestService internalTestService = new InternalTestService();
            internalTestService.Add(1, 2).ShouldBe(3);
        }
    }
}

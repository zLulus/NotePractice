using System;
using Xunit;

namespace DotNetCore.xUnit.Test
{
    public class AddUnitTest
    {
        //×ÊÁÏ£ºhttps://xunit.github.io/docs/getting-started-dotnet-core
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}

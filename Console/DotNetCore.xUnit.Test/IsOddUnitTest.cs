using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DotNetCore.xUnit.Test
{
    public class IsOddUnitTest
    {
        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }
    }
}

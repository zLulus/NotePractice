using DotNetCore.NUnit.Test.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore.NUnit.Test
{
    [TestFixture]
    public class PrimeService_IsPrimeShould_UnitTest
    {
        //资料：https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-nunit
        //控制台需要删除Program.cs文件
        //引用包包括：
        //NUnit/NUnit3TestAdapter/Microsoft.NET.Test.Sdk
        private readonly PrimeService _primeService;

        public PrimeService_IsPrimeShould_UnitTest()
        {
            _primeService = new PrimeService();
        }

        [Test]
        public void ReturnFalseGivenValueOf1()
        {
            var result = _primeService.IsPrime(1);

            Assert.IsFalse(result, "1 should not be prime");
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void ReturnFalseGivenValuesLessThan2(int value)
        {
            var result = _primeService.IsPrime(value);

            Assert.IsFalse(result, $"{value} should not be prime");
        }
    }
}

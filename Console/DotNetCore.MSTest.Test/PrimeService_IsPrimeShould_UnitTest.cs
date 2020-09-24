using DotNetCore.MSTest.Test.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCore.MSTest.Test
{
    [TestClass]
    public class PrimeService_IsPrimeShould_UnitTest
    {
        //×ÊÁÏ£ºhttps://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest

        private readonly PrimeService _primeService;

        public PrimeService_IsPrimeShould_UnitTest()
        {
            _primeService = new PrimeService();
        }

        [TestMethod]
        public void ReturnFalseGivenValueOf1()
        {
            var result = _primeService.IsPrime(1);

            Assert.IsFalse(result, "1 should not be prime");
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(1)]
        public void ReturnFalseGivenValuesLessThan2(int value)
        {
            var result = _primeService.IsPrime(value);

            Assert.IsFalse(result, $"{value} should not be prime");
        }
    }
}

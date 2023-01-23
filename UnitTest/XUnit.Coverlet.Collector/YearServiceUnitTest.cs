using UnitTest.ClassLibrary;

namespace XUnit.Coverlet.Collector
{
    public class YearServiceUnitTest
    {
        readonly YearService _yearService;

        public YearServiceUnitTest() => _yearService = new YearService();

        [
            Theory,
            InlineData(2000), InlineData(1600)
        ]
        public void IsLeapYear_WholeCenturyOfNumbers_ReturnTrue(int value) =>
            Assert.True(_yearService.IsLeapYear(value), $"{value} is leap year.");

        [
            Theory,
            InlineData(1900), InlineData(1700)
        ]
        public void IsLeapYear_WholeCenturyOfNumbers_ReturnFalse(int value) =>
            Assert.False(_yearService.IsLeapYear(value), $"{value} is not leap year.");

        [
           Theory,
           InlineData(2020)
       ]
        public void IsPrime_NotWholeCenturyOfNumbers_ReturnTrue(int value) =>
           Assert.True(_yearService.IsLeapYear(value), $"{value} is leap year.");

        [
            Theory,
            InlineData(2022), InlineData(2003)
        ]
        public void IsPrime_NotWholeCenturyOfNumbers_ReturnFalse(int value) =>
            Assert.False(_yearService.IsLeapYear(value), $"{value} is not leap year.");
    }
}

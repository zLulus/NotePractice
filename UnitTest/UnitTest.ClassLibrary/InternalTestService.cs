using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("XUnit.Coverlet.Collector")]
namespace UnitTest.ClassLibrary
{
    internal class InternalTestService
    {
        internal int Add(int number1, int number2)
        {
            return number1 + number2;
        }
    }
}

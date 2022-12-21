namespace UnitTest.ClassLibrary
{
    public class YearService
    {
        public bool IsLeapYear(int year)
        {
            if (year % 4 == 0)
                if (year % 100 == 0)
                    if (year % 400 == 0)
                        return true;
                    else
                        return false;
                else
                    return true;
            return false;
        }
    }
}

namespace DotNet7.CodeLibrary.TimeSpanTest
{
    public class TimeSpanTestDemo
    {
        public static void Run()
        {
            Calculation(2022, 10);
            Calculation(2024, 1);
        }

        private static void Calculation(int year, int month)
        {
            var firstTime = new DateTime(year, month, 1);
            DateTime time;
            if (firstTime <= DateTime.Now)
            {
                time = DateTime.MinValue + (DateTime.Now - firstTime);
            }
            else
            {
                time = DateTime.MinValue + (firstTime - DateTime.Now);
            }
            Console.WriteLine($"Total Month: {(time.Year - 1) * 12 + time.Month - 1}");
            //DateTime.MinValue=1 year 1 month 1 day, so we need to year-1,month-1,day-1
            Console.WriteLine($"Year:{time.Year - 1},Month:{time.Month - 1},Day:{time.Day - 1}");
        }
    }
}

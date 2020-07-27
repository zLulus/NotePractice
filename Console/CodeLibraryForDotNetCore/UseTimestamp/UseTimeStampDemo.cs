using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseTimestamp
{
    public class UseTimeStampDemo
    {
        public static void Run()
        {
            TimeStampConvertEnum timeStampConvert = TimeStampConvertEnum.Second;
            var timeStamp = GetTimestamp(timeStampConvert);
            var t = GetTime(timeStamp, timeStampConvert);
        }

        private static DateTime GetTime(long time, TimeStampConvertEnum timeStampConvert)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            //时间戳按秒计算
            DateTime dt;
            if(timeStampConvert==TimeStampConvertEnum.Second)
                dt= startTime.AddSeconds(time);
            else
                dt = startTime.AddMilliseconds(time);
            return dt;
        }

        private static long GetTimestamp(TimeStampConvertEnum timeStampConvert)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp;
            if (timeStampConvert == TimeStampConvertEnum.Second)
                timeStamp = (long)(DateTime.Now - startTime).TotalSeconds; // 相差秒数
            else
                timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds; // 相差秒数
            return timeStamp;
        }
    }
}

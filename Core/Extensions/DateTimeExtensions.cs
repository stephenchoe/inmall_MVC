using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ConvertToTaipeiTime(this DateTime input)
        {
            string taipeiTimeZoneId = "Taipei Standard Time";
            TimeZoneInfo taipeiTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(taipeiTimeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(input.ToUniversalTime(), taipeiTimeZoneInfo);

        }
        public static int ToDateNumber(this DateTime input)
        {
            return Convert.ToInt32(GetDateString(input.Date));
        }
        public static int ToTimeNumber(this DateTime input)
        {
            return Convert.ToInt32(GetTimeString(input));
        }

        public static string GetDateString(DateTime dateTime)
        {
            string year = dateTime.Year.ToString();
            string month = dateTime.Month.ToString();
            string day = dateTime.Day.ToString();

            if (dateTime.Month < 10) month = "0" + month;
            if (dateTime.Day < 10) day = day = "0" + day;


            return year + month + day;

        }
        static string GetTimeString(DateTime dateTime)
        {
            string hour = dateTime.Hour.ToString();
            string minute = dateTime.Minute.ToString();
            string second = dateTime.Second.ToString();
            string mileSecond = dateTime.Millisecond.ToString();

            if (dateTime.Hour < 10) hour = "0" + hour;
            if (dateTime.Minute < 10) minute = "0" + minute;
            if (dateTime.Second < 10) second = "0" + second;

            if (dateTime.Millisecond < 10)
            {
                mileSecond = "00" + mileSecond;
            }
            else if (dateTime.Millisecond < 100)
            {
                mileSecond = "0" + mileSecond;
            }

            return hour + minute + second + mileSecond;

        }



    }
}

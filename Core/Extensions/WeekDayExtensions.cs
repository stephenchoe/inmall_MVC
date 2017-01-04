using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class WeekDayExtensions
    {
        public static string ConvertToChinese(this DayOfWeek input)
        {
            switch (input)
            {
                case DayOfWeek.Sunday:
                    return "星期日";
                case DayOfWeek.Monday:
                    return "星期一";
                case DayOfWeek.Tuesday:
                    return "星期二";
                case DayOfWeek.Wednesday:
                    return "星期三";
                case DayOfWeek.Thursday:
                    return "星期四";
                case DayOfWeek.Friday:
                    return "星期五";
                case DayOfWeek.Saturday:
                    return "星期六";

                default:
                    return "星期日";
            }
        }

        public static DayOfWeek ConvertToDayOfWeek(this string input)
        {
            if (input == DayOfWeek.Monday.ToString()) return DayOfWeek.Monday;
            if (input == DayOfWeek.Tuesday.ToString()) return DayOfWeek.Tuesday;
            if (input == DayOfWeek.Wednesday.ToString()) return DayOfWeek.Wednesday;
            if (input == DayOfWeek.Thursday.ToString()) return DayOfWeek.Thursday;
            if (input == DayOfWeek.Friday.ToString()) return DayOfWeek.Friday;
            if (input == DayOfWeek.Saturday.ToString()) return DayOfWeek.Saturday;
            if (input == DayOfWeek.Sunday.ToString()) return DayOfWeek.Sunday;

            throw new InvalidOperationException("轉換WeekDay失敗");
        }
        public static DateTime GetWeekdayOfMonth(this DayOfWeek weekday, int year, int month, int occurrence)
        {
            var fday = new DateTime(year, month, 1);

            var fOc = fday.DayOfWeek == weekday ? fday : fday.AddDays(weekday - fday.DayOfWeek);
            if (fOc.Month < month) occurrence = occurrence + 1;

            return fOc.AddDays(7 * (occurrence - 1));
        }

    }
}

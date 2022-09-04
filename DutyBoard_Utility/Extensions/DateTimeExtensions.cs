using System;
using System.Globalization;

namespace DutyBoard_Utility.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetDayOfWeek(this DateTime date)
        {
            return date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek;
        }

        public static int GetWeekOfYear(this DateTime date)
        {
            var cal = new GregorianCalendar();
            return cal.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, System.DayOfWeek.Monday);
        }
    }
}

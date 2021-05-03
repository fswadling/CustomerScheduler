using System;
using System.Collections.Generic;

namespace CustomerPreferences.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime? SetDayNumber(this DateTime dateTime, int dayNumber)
        {
            try
            {
                return new DateTime(dateTime.Year, dateTime.Month, dayNumber);
            }
            catch
            {
                return null;
            }
        }

        public static IEnumerable<DateTime> Range(this DateTime dateTime, DateTime end)
        {
            DateTime date = dateTime;

            while (date <= end)
            {
                yield return date;
                date = date.AddDays(1);
            }
        }
    }
}

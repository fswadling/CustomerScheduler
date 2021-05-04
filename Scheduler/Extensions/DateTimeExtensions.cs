using CustomerPreferences.Types;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<DateTime> AllDatesAfter(this DateTime dateTime)
        {
            DateTime date = dateTime;

            while (true)
            {
                yield return date;
                date = date.AddDays(1);
            }
        }

        public static IEnumerable<DateTime> DatesOnDayInMonthAfter(this DateTime dateTime, int dayOfMonth)
        {
            // Can assume start of month exists.
            DateTime startOfMonth = dateTime.SetDayNumber(1)!.Value;

            DateTime? date = startOfMonth.SetDayNumber(dayOfMonth);

            // If an invalid day number is attempted, e.g 31 of September, SetDayNumber will return null and be ignored. 
            if (date.HasValue && date.Value >= dateTime)
            {
                yield return date.Value;
            }

            startOfMonth = startOfMonth.AddMonths(1);

            while (true)
            {
                date = startOfMonth.SetDayNumber(dayOfMonth);

                if (date.HasValue)
                {
                    yield return date.Value;
                }

                startOfMonth = startOfMonth.AddMonths(1);
            }
        }

        public static IEnumerable<DateTime> DatesOnSelectedDaysAfter(this DateTime dateRange, DaySelection daySelection)
        {
            if (!daySelection.Any())
            {
                yield break;
            }

            var dayDates = new List<DateTime>(capacity: 7);

            var date = dateRange;

            for (int i = 0; i < 7; i++)
            {
                if (daySelection.Contains(date.DayOfWeek))
                {
                    dayDates.Add(date);
                    yield return date;
                }

                date = date.AddDays(1);
            }

            while (true)
            {
                for (int i = 0; i < dayDates.Count; i++)
                {
                    date = dayDates[i] = dayDates[i].AddDays(7);
                    yield return date;
                }
            }
        }
    }
}

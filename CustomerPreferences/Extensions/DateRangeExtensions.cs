using CustomerPreferences.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerPreferences.Extensions
{
    public static class DateRangeExtensions
    {
        public static IEnumerable<DateTime> GetAllDatesInRange(this DateRange dateRange) =>
            dateRange.Start.Range(dateRange.End);

        public static IEnumerable<DateTime> GetDatesOnDayInMonthInRange(this DateRange dateRange, int dayOfMonth)
        {
            // Can assume start of month exists.
            DateTime startOfMonth = dateRange.Start.SetDayNumber(1)!.Value;

            DateTime? date = startOfMonth.SetDayNumber(dayOfMonth);

            // If an invalid day number is attempted, e.g 31 of September, SetDayNumber will return null and be ignored. 
            if (date.HasValue && date.Value >= dateRange.Start && date.Value <= dateRange.End)
            {
                yield return date.Value;
            }

            startOfMonth = startOfMonth.AddMonths(1);

            while (startOfMonth <= dateRange.End)
            {
                date = startOfMonth.SetDayNumber(dayOfMonth);

                if (date.HasValue && date.Value <= dateRange.End)
                {
                    yield return date.Value;
                }

                startOfMonth = startOfMonth.AddMonths(1);
            }
        }

        public static IEnumerable<DateTime> GetDatesOnSelectedDays(this DateRange dateRange, DaySelection daySelection)
        {
            if (!daySelection.Any())
            {
                yield break;
            }

            var dayDates = new List<DateTime>(capacity: 7);

            var date = dateRange.Start;

            for (int i = 0; i < 7; i++)
            {
                if (daySelection.Contains(date.DayOfWeek) && date <= dateRange.End)
                {
                    dayDates.Add(date);
                    yield return date;
                }
                else if (date > dateRange.End)
                {
                    break;
                }

                date = date.AddDays(1);
            }

            while (date < dateRange.End)
            {
                for (int i = 0; i < dayDates.Count; i++)
                {
                    date = dayDates[i] = dayDates[i].AddDays(7);

                    if (date > dateRange.End)
                    {
                        break;
                    }

                    yield return date;
                }
            }
        }
    }
}

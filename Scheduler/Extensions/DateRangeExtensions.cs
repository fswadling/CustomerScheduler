using CustomerPreferences.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerPreferences.Extensions
{
    public static class DateRangeExtensions
    {
        public static IEnumerable<DateTime> AllDatesInRange(this DateRange dateRange) =>
            dateRange.Start.AllDatesAfter()
                .TakeWhile(date => date <= dateRange.End);

        public static IEnumerable<DateTime> DatesOnDayInMonthInRange(this DateRange dateRange, int dayOfMonth) =>
            dateRange.Start.DatesOnDayInMonthAfter(dayOfMonth)
                .TakeWhile(date => date <= dateRange.End);

        public static IEnumerable<DateTime> DatesOnSelectedDays(this DateRange dateRange, DaySelection daySelection) =>
            dateRange.Start.DatesOnSelectedDaysAfter(daySelection)
                .TakeWhile(date => date <= dateRange.End);
    }
}

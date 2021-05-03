using CustomerPreferences.Extensions;
using CustomerPreferences.Types;
using System;
using System.Collections.Generic;

namespace CustomerPreferences.Preferences
{
    public class DayOfTheMonth : IPreference
    {
        public int Day { get; init; } = 1;

        public IEnumerable<DateTime> GetDatesInRange(DateRange dateRange) =>
            dateRange.GetDatesOnDayInMonthInRange(this.Day);
    }
}

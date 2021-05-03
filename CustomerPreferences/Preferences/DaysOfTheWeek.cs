using CustomerPreferences.Types;
using CustomerPreferences.Extensions;
using System;
using System.Collections.Generic;

namespace CustomerPreferences.Preferences
{
    public class DaysOfTheWeek : IPreference
    {
        public DaySelection DaySelection { get; init; } = new DaySelection();

        public IEnumerable<DateTime> GetDatesInRange(DateRange dateRange) =>
            dateRange.GetDatesOnSelectedDays(this.DaySelection);
    }
}

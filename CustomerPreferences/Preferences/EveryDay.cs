using CustomerPreferences.Extensions;
using CustomerPreferences.Types;
using System;
using System.Collections.Generic;

namespace CustomerPreferences.Preferences
{
    public class EveryDay : IPreference 
    {
        public IEnumerable<DateTime> GetDatesInRange(DateRange dateRange) =>
            dateRange.GetAllDatesInRange();
    }
}

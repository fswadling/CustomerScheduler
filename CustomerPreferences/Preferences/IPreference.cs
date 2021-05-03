using CustomerPreferences.Types;
using System;
using System.Collections.Generic;

namespace CustomerPreferences.Preferences
{
    public interface IPreference
    {
        IEnumerable<DateTime> GetDatesInRange(DateRange dateRange);
    }
}

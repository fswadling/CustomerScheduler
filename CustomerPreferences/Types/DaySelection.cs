using System;
using System.Collections.Generic;

namespace CustomerPreferences.Types
{
    public class DaySelection : HashSet<DayOfWeek>
    {
        public DaySelection()
            : base()
        { }

        public DaySelection(IEnumerable<DayOfWeek> daysOfWeek)
            : base(daysOfWeek)
        { }

    }
}

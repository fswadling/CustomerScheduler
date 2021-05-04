using System;

namespace CustomerPreferences.Types
{
    public class DateRange
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public DateRange(DateTime start, DateTime end)
        {
            if (end < start)
            {
                throw new ArgumentException("The start date must be before the end date");
            }

            this.Start = start;
            this.End = end;
        }
    }
}

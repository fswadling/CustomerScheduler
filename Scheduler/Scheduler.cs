using CustomerPreferences.Types;
using System.Collections.Generic;

namespace CustomerPreferences
{
    public static class Scheduler
    {
        public static Schedule CreateSchedule(IEnumerable<CustomerPreference> customerPreferences, DateRange dateRange)
        {
            var customersOnDates = new Schedule();

            foreach (var customerPreference in customerPreferences)
            {
                var datesForCustomer = customerPreference.Preference.GetDatesInRange(dateRange);

                foreach (var date in datesForCustomer)
                {
                    // Dates are inserted in order as customersOnDates is an SortedDictionary
                    customersOnDates.AddCustomerOnDate(date, customerPreference.Customer);
                }
            }

            return customersOnDates;
        }
    }
}

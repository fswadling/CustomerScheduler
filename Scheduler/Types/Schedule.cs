using System;
using System.Collections.Generic;

namespace CustomerPreferences.Types
{
    public class Schedule : SortedDictionary<DateTime, List<string>>
    {
        public void AddCustomerOnDate(DateTime date, string customer)
        {
            if (!this.ContainsKey(date))
            {
                this.Add(date, new List<string> { customer });
            }
            else
            {
                this[date].Add(customer);
            }
        }
    }
}

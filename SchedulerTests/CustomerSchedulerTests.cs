using CustomerPreferences;
using CustomerPreferences.Preferences;
using CustomerPreferences.Types;
using System;
using System.Collections.Generic;
using Xunit;

namespace SchedulerTests
{
    public class CustomerSchedulerTests
    {
        [Fact]
        public void Schedule_should_be_populated_correctly_with_multiple_customers()
        {
            List<CustomerPreference> preferences = new()
            {
                new() { Customer = "Jack", Preference = new DayOfTheMonth { Day = 1 } },
                new() { Customer = "George", Preference = new DaysOfTheWeek { DaySelection = new() { DayOfWeek.Monday, DayOfWeek.Wednesday } } },
                new() { Customer = "William", Preference = new EveryDay() }
            };

            var schedule = Scheduler.CreateSchedule(preferences, new(start: new DateTime(2020, 1, 1), end: new DateTime(2020, 1, 6)));

            Assert.Contains(new DateTime(2020, 1, 1), schedule.Keys);
            var entry = schedule[new DateTime(2020, 1, 1)];
            Assert.Contains("George", entry);
            Assert.Contains("Jack", entry);
            Assert.Contains("William", entry);
            Assert.Equal(3, entry.Count);

            entry = schedule[new DateTime(2020, 1, 2)];
            Assert.Contains("William", entry);
            Assert.Single(entry);

            entry = schedule[new DateTime(2020, 1, 3)];
            Assert.Contains("William", entry);
            Assert.Single(entry);

            entry = schedule[new DateTime(2020, 1, 4)];
            Assert.Contains("William", entry);
            Assert.Single(entry);

            entry = schedule[new DateTime(2020, 1, 5)];
            Assert.Contains("William", entry);
            Assert.Single(entry);

            entry = schedule[new DateTime(2020, 1, 6)];
            Assert.Contains("George", entry);
            Assert.Contains("William", entry);
            Assert.Equal(2, entry.Count);
        }
    }
}

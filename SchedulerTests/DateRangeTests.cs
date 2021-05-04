using CustomerPreferences.Types;
using CustomerPreferences.Extensions;
using System;
using Xunit;
using System.Linq;

namespace SchedulerTests
{
    public class DateRangeTests
    {
        [Fact]
        public void AllDatesInRange_should_return_all_dates_within_a_given_range()
        {
            var dateRange = new DateRange(start: new DateTime(2020, 1, 1), end: new DateTime(2020, 1, 5));
            var dates = dateRange.AllDatesInRange().ToList();
            Assert.Contains(new DateTime(2020, 1, 1), dates);
            Assert.Contains(new DateTime(2020, 1, 2), dates);
            Assert.Contains(new DateTime(2020, 1, 3), dates);
            Assert.Contains(new DateTime(2020, 1, 4), dates);
            Assert.Contains(new DateTime(2020, 1, 5), dates);
            Assert.Equal(5, dates.Count);

            dateRange = new DateRange(start: new DateTime(2019, 12, 28), end: new DateTime(2020, 1, 2));
            dates = dateRange.AllDatesInRange().ToList();
            Assert.Contains(new DateTime(2019, 12, 28), dates);
            Assert.Contains(new DateTime(2019, 12, 29), dates);
            Assert.Contains(new DateTime(2019, 12, 30), dates);
            Assert.Contains(new DateTime(2019, 12, 31), dates);
            Assert.Contains(new DateTime(2020, 1, 1), dates);
            Assert.Contains(new DateTime(2020, 1, 2), dates);
            Assert.Equal(6, dates.Count);
        }

        [Fact]
        public void AllDatesInRange_should_return_one_result_if_the_date_range_covers_one_day()
        {
            var dateRange = new DateRange(start: new DateTime(2020, 1, 1), end: new DateTime(2020, 1, 1));
            var dates = dateRange.AllDatesInRange().ToList();
            Assert.Contains(new DateTime(2020, 1, 1), dates);
            Assert.Single(dates);
        }

        [Fact]
        public void Invalid_DateRange_arguments_should_throw_an_exception()
        {
            Action createRange = () => new DateRange(start: new DateTime(2020, 1, 2), end: new DateTime(2020, 1, 1));
            Assert.Throws<ArgumentException>(createRange);
        }

        [Fact]
        public void DatesOnDayInMonthInRange_should_return_correct_dates()
        {
            var dateRange = new DateRange(start: new DateTime(2020, 1, 10), end: new DateTime(2020, 4, 5));
            var dates = dateRange.DatesOnDayInMonthInRange(1).ToList();
            Assert.Contains(new DateTime(2020, 2, 1), dates);
            Assert.Contains(new DateTime(2020, 3, 1), dates);
            Assert.Contains(new DateTime(2020, 4, 1), dates);
            Assert.Equal(3, dates.Count);
        }

        [Fact]
        public void DatesOnDayInMonthInRange_should_handle_dates_that_dont_exist_in_some_months()
        {
            var dateRange = new DateRange(start: new DateTime(2020, 1, 10), end: new DateTime(2020, 4, 5));
            var dates = dateRange.DatesOnDayInMonthInRange(31).ToList();
            Assert.Contains(new DateTime(2020, 1, 31), dates);
            Assert.Contains(new DateTime(2020, 3, 31), dates);
            Assert.Equal(2, dates.Count);
        }

        [Fact]
        public void DatesOnDayInMonthInRange_should_return_empty_if_given_a_small_date_range_not_containing_the_date()
        {
            var dateRange = new DateRange(start: new DateTime(2020, 1, 1), end: new DateTime(2020, 1, 5));
            var dates = dateRange.DatesOnDayInMonthInRange(31).ToList();
            Assert.Empty(dates);
        }

        [Fact]
        public void DatesOnDayInMonthInRange_should_return_a_single_date_if_given_a_small_range()
        {
            var dateRange = new DateRange(start: new DateTime(2020, 1, 1), end: new DateTime(2020, 1, 5));
            var dates = dateRange.DatesOnDayInMonthInRange(5).ToList();
            Assert.Single(dates);

            dates = dateRange.DatesOnDayInMonthInRange(1).ToList();
            Assert.Single(dates);
        }

        [Fact]
        public void DatesOnSelectedDays_should_only_return_the_selected_day()
        {
            var dateRange = new DateRange(start: new DateTime(2015, 1, 1), end: new DateTime(2020, 12, 31));
            var dates = dateRange.DatesOnSelectedDays(new() { DayOfWeek.Monday }).ToList();
            Assert.All(dates, date => Assert.Equal(DayOfWeek.Monday, date.DayOfWeek));

            dates = dateRange.DatesOnSelectedDays(new() { DayOfWeek.Tuesday }).ToList();
            Assert.All(dates, date => Assert.Equal(DayOfWeek.Tuesday, date.DayOfWeek));

            dates = dateRange.DatesOnSelectedDays(new() { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Saturday }).ToList();
            var selectedDays = dates.Select(d => d.DayOfWeek).ToHashSet();
            Assert.Contains(DayOfWeek.Monday, selectedDays);
            Assert.Contains(DayOfWeek.Wednesday, selectedDays);
            Assert.Contains(DayOfWeek.Saturday, selectedDays);
            Assert.Equal(3, selectedDays.Count);
        }

        [Fact]
        public void DatesOnSelectedDays_should_return_selected_dates_when_given_a_small_range_of_dates()
        {
            var dateRange = new DateRange(start: new DateTime(2020, 1, 1), end: new DateTime(2020, 1, 3));
            var dates = dateRange.DatesOnSelectedDays(new() { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }).ToList();
            Assert.Equal(3, dates.Count);
            Assert.Contains(new DateTime(2020, 1, 1), dates);
            Assert.Contains(new DateTime(2020, 1, 2), dates);
            Assert.Contains(new DateTime(2020, 1, 3), dates);
        }

        [Fact]
        public void DatesOnSelectedDays_should_return_empty_collection_when_given_a_small_range_of_dates_that_do_not_include_the_selected_days()
        {
            var dateRange = new DateRange(start: new DateTime(2020, 1, 1), end: new DateTime(2020, 1, 3));
            var dates = dateRange.DatesOnSelectedDays(new() { DayOfWeek.Monday }).ToList();
            Assert.Empty(dates);
        }

        [Fact]
        public void DatesOnSelectedDays_should_return_one_date_if_the_date_range_covers_one_date()
        {
            var dateRange = new DateRange(start: new DateTime(2020, 1, 1), end: new DateTime(2020, 1, 1));
            var dates = dateRange.DatesOnSelectedDays(new() { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }).ToList();
            Assert.Single(dates);
            Assert.Contains(new DateTime(2020, 1, 1), dates);
        }

        [Fact]
        public void DatesOnSelectedDays_should_return_all_dates_of_a_given_range_with_a_given_day()
        {
            var dateRange = new DateRange(start: new DateTime(2020, 1, 1), end: new DateTime(2020, 2, 1));
            var dates = dateRange.DatesOnSelectedDays(new() { DayOfWeek.Monday }).ToList();
            Assert.Equal(4, dates.Count);
        }

        [Fact]
        public void DatesOnSelectedDays_should_return_empty_collection_if_no_days_are_given()
        {
            var dateRange = new DateRange(start: new DateTime(2015, 1, 1), end: new DateTime(2020, 12, 31));
            var dates = dateRange.DatesOnSelectedDays(new()).ToList();
            Assert.Empty(dates);
        }
    }
}

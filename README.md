# CustomerScheduler

An application to produce a schedule of when to deliver customers information depending on what preferences they select. The actual logic is exposed in the Scheduler class library. SchedulerAPI exposes a simple REST API to run the logic through. A schedule can be obtained by doing a POST to /CustomerScheduler with the customer preferences set in the JSON body. Some examples are given below:

[
  {
    "customer": "Fred",
    "preference": {},
    "preferenceType": "everyDay"
  }
]

This will retrieve a schedule for a single customer with a selected preference for every day. As this requires no arguments, the preference field remains empty.

[
  {
    "customer": "Jack",
    "preference": { "selectedDays": [ "monday", "tuesday" ] },
    "preferenceType": "daysOfTheWeek"
  }
]

This will retrieve a schedule for a single customer with a selected preference for mondays and tuesdays every week.

[
  {
    "customer": "George",
    "preference": { "day": 1 },
    "preferenceType": "dayOfTheMonth"
  }
]

This will retrieve a schedule for a single customer with a selected preference for the first of every month.

[
  {
    "customer": "Fred",
    "preference": {},
    "preferenceType": "everyDay"
  },
  {
    "customer": "Jack",
    "preference": { "selectedDays": [ "monday", "tuesday" ] },
    "preferenceType": "daysOfTheWeek"
  },
  {
    "customer": "George",
    "preference": { "day": 1 },
    "preferenceType": "dayOfTheMonth"
  }
]

This will return a schedule for all three customers in one query. 

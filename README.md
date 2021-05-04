# CustomerScheduler

An application to produce a schedule of when to deliver customers information depending on what preferences they select. The actual logic is exposed in the Scheduler class library. SchedulerAPI exposes a simple REST API to run the logic through. A schedule can be obtained by doing a POST to /CustomerScheduler with the customer preferences set in the JSON body. Some examples are given below:

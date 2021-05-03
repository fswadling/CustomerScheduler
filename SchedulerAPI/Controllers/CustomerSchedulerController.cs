using CustomerPreferences;
using Microsoft.AspNetCore.Mvc;
using SchedulerAPI.Dtos;
using System;
using System.Collections.Generic;

namespace SchedulerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerSchedulerController : ControllerBase
    {
        [HttpPost]
        public ScheduleDTO GetSchedule(List<CustomerPreferenceDTO> customerPreferences) =>
            Scheduler.CreateSchedule(customerPreferences, new(start: DateTime.Today, end: DateTime.Today.AddDays(90)))
                .ToScheduleDTO();
    }
}

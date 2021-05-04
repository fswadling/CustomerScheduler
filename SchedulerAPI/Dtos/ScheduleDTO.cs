using System.Collections.Generic;

namespace SchedulerAPI.Dtos
{
    public class ScheduleDTO : List<KeyValuePair<string, List<string>>>
    {
        public ScheduleDTO()
        { }

        public ScheduleDTO(IEnumerable<KeyValuePair<string, List<string>>> scheduleRows) : base(scheduleRows)
        { }
    }
}

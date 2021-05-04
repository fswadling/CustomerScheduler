using CustomerPreferences.Types;
using System.Linq;

namespace SchedulerAPI.Dtos
{
    public static class DTOGenerators
    {
        public static ScheduleDTO ToScheduleDTO(this Schedule schedule)
        {
            var keyValuesPairs = schedule.ToDictionary(x => x.Key.ToShortDateString(), x => x.Value);
            var dto = new ScheduleDTO(keyValuesPairs);
            return dto;
        }
    }
}

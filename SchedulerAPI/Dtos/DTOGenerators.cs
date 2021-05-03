using CustomerPreferences.Types;
using System.Linq;

namespace SchedulerAPI.Dtos
{
    public static class DTOGenerators
    {
        public static ScheduleDTO ToScheduleDTO(this Schedule schedule)
        {
            var dto = new ScheduleDTO();
            var keyValuesPairs = schedule.ToDictionary(x => x.Key.ToShortDateString(), x => x.Value);
            dto.AddRange(keyValuesPairs);
            return dto;
        }
    }
}

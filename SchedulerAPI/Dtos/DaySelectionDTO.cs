using System.Collections.Generic;

namespace SchedulerAPI.Dtos
{
    public record DaySelectionDTO
    {
        public List<string> SelectedDays { get; init; } = new List<string>();
    }
}

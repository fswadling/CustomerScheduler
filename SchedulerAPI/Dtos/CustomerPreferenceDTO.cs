using CustomerPreferences.Types;
using SchedulerAPI.JsonConverter;
using System.Text.Json.Serialization;

namespace SchedulerAPI.Dtos
{
    // Note: On a large scale system I would probably insist on a hard boundary between DTOs and the model. But on a project of this size
    // I don't see the point.
    [JsonConverter(typeof(PreferenceConverter))]
    public record CustomerPreferenceDTO : CustomerPreference
    {
        public const string EveryDay = "everyday";
        public const string DayOfTheMonth = "dayofthemonth";
        public const string DaysOfTheWeek = "daysoftheweek";

        public string PreferenceType { get; init; } = EveryDay;
    }
}

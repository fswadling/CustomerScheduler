using CustomerPreferences.Preferences;

namespace CustomerPreferences.Types
{
    public record CustomerPreference
    {
        public string Customer { get; init; } = string.Empty;

        public IPreference Preference { get; init; } = new EveryDay();
    }
}

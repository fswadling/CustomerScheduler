using CustomerPreferences.Preferences;
using CustomerPreferences.Types;
using SchedulerAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchedulerAPI.JsonConverter
{
    // Need a custom json converter for preferences as the default converter can't handle polymorphic deserialization.
    public class PreferenceConverter : JsonConverter<CustomerPreferenceDTO>
    {
        public override CustomerPreferenceDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            using var jsonDocument = JsonDocument.ParseValue(ref reader);

            var typeProperty = GetPropertyIgnoreCase(jsonDocument, nameof(CustomerPreferenceDTO.PreferenceType)) ?? throw new JsonException();

            var typeDifferentiator = typeProperty.Value.GetString() ?? throw new JsonException();

            var preference = GetReadPreferenceUsingDifferentiator(typeDifferentiator, jsonDocument);

            var customerProperty = GetPropertyIgnoreCase(jsonDocument, nameof(CustomerPreferenceDTO.Customer)) ?? throw new JsonException();

            var customer = customerProperty.Value.GetString() ?? throw new JsonException();

            return new() { Customer = customer, Preference = preference, PreferenceType = typeDifferentiator };
        }

        public override void Write(Utf8JsonWriter writer, CustomerPreferenceDTO value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, options);
        }

        private static Type? GetTypeFromDifferentiator(string preferenceDifferentiator) =>
            preferenceDifferentiator.ToLower() switch
            {
                CustomerPreferenceDTO.EveryDay => typeof(EveryDay),
                CustomerPreferenceDTO.DaysOfTheWeek => typeof(DaysOfTheWeek),
                CustomerPreferenceDTO.DayOfTheMonth => typeof(DayOfTheMonth),
                _ => null
            };

        private static JsonProperty? GetPropertyIgnoreCase(JsonDocument jsonDocument,  string propertyName)
        {
            var properties = jsonDocument.RootElement
               .EnumerateObject()
               .Where(p => string.Compare(p.Name, propertyName, StringComparison.OrdinalIgnoreCase) == 0)
               .ToList();

            return properties.Any()
                ? properties.Single()
                : null;
        }

        private static IPreference GetReadPreferenceUsingDifferentiator(string typeDifferentiator, JsonDocument jsonDocument)
        {
            var preferenceType = GetTypeFromDifferentiator(typeDifferentiator) ?? throw new JsonException();

            var preferenceProperty = GetPropertyIgnoreCase(jsonDocument, nameof(CustomerPreferenceDTO.Preference)) ?? throw new JsonException();

            var preferenceRawText = preferenceProperty.Value.GetRawText() ?? throw new JsonException();

            // DaysOfTheWeek needs a special serializer as HashSets of enums do not have a nice cefault serialization.
            return preferenceType == typeof(DaysOfTheWeek)
                ? DeserializeDaysOfTheWeek(preferenceRawText)
                : (IPreference)(JsonSerializer.Deserialize(preferenceRawText, preferenceType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? throw new JsonException());
        }

        private static IPreference DeserializeDaysOfTheWeek(string preferenceRawText)
        {
            var dto = JsonSerializer.Deserialize<DaySelectionDTO>(preferenceRawText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true } ) ?? throw new JsonException();

            List<DayOfWeek> days;
            try
            {
                days = dto.SelectedDays.Select(dayString => Enum.Parse<DayOfWeek>(dayString, ignoreCase: true)).ToList();
            }
            catch (Exception e)
            {
                throw new JsonException("Error deserializing days of the week", e);
            }

            var daySelection = new DaySelection();

            foreach (var day in days)
            {
                daySelection.Add(day);
            }

            return new DaysOfTheWeek { DaySelection = daySelection };
        }
    }
}

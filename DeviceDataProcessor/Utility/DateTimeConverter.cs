using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DeviceDataProcessor.Utility
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(),
                "MM-dd-yyyy hh:mm:ss", CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(
                "MM-dd-yyyy hh:mm:ss", CultureInfo.InvariantCulture));
        }
    }
}

// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CodeRabbits.KaoList.Web;

public partial class DateTimeJsonConverter : JsonConverter<DateTime?>
{
    private static readonly Regex _dateFormatRegex = GeneratedDateFormatRegex();
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString();
        if (string.IsNullOrEmpty(dateString) || dateString == "0000-00-00")
        {
            return DateTime.MinValue;
        }

        if (_dateFormatRegex.IsMatch(dateString) && DateTime.TryParse(dateString, out var date))
        {
            return date;
        }

        return DateTime.MinValue;
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        var culture = new CultureInfo("ko-kr");
        writer.WriteStringValue(value?.ToString("yyyy-MM-dd", culture));
    }

    [GeneratedRegex("([0-9]{4})-([0-9]{2})-([0-9]{2})")]
    private static partial Regex GeneratedDateFormatRegex();
}

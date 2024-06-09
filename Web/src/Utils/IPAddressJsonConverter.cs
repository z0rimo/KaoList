// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeRabbits.KaoList.Web.Utils;

public class IPAddressJsonConverter : JsonConverter<IPAddress>
{
    private readonly ILogger<IPAddressJsonConverter> _logger;

    public IPAddressJsonConverter(ILogger<IPAddressJsonConverter> logger)
    {
        _logger = logger;
    }

    public override IPAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var ipAddressAsString = reader.GetString();
        if (ipAddressAsString == null)
        {
            _logger.LogWarning("Encountered null IP address in JSON, substituting with '0.0.0.0'.");
            return IPAddress.Parse("0.0.0.0");
        }
        return IPAddress.Parse(ipAddressAsString);
    }

    public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

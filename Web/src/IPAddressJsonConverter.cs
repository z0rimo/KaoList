// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

public class IPAddressJsonConverter : JsonConverter<IPAddress>
{
    public override IPAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string ipAddressAsString = reader.GetString();
        return IPAddress.Parse(ipAddressAsString);
    }

    public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

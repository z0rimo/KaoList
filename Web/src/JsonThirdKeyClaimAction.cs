// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System.Security.Claims;
using System.Text.Json;

namespace CodeRabbits.KaoList.Web;

public class JsonThirdKeyClaimAction : JsonSubKeyClaimAction
{
    /// <summary>
    /// Creates a new JsonThirdKeyClaimAction.
    /// </summary>
    /// <param name="claimType">The value to use for Claim.Type when creating a Claim.</param>
    /// <param name="valueType">The value to use for Claim.ValueType when creating a Claim.</param>
    /// <param name="jsonKey">The top level key to look for in the json user data.</param>
    /// <param name="subKey">The second level key to look for in the json user data.</param>
    /// <param name="thirdKey">The third level key to look for in the json user data.</param>
    public JsonThirdKeyClaimAction(string claimType, string valueType, string jsonKey, string subKey, string thirdKey) : base(claimType, valueType, jsonKey, subKey)
    {
        ThirdKey = thirdKey;
    }

    /// <summary>
    /// The third level key to look for in the json user data.
    /// </summary>
    public string ThirdKey { get; }

    /// <inheritdoc />
    public override void Run(JsonElement userData, ClaimsIdentity identity, string issuer)
    {
        var value = GetValue(userData, JsonKey, SubKey, ThirdKey);
        if (!string.IsNullOrEmpty(value))
        {
            identity.AddClaim(new Claim(ClaimType, value, ValueType, issuer));
        }
    }

    // Get the given subProperty from a property.
    private static JsonElement? GetSubProperty(JsonElement userData, string propertyName, string subProperty)
    {
        if (userData.TryGetProperty(propertyName, out var value)
            && value.ValueKind == JsonValueKind.Object && value.TryGetProperty(subProperty, out value))
        {
            return value;
        }
        return null;
    }

    // Get the given thirdProperty from a property.
    private static string? GetValue(JsonElement userData, string propertyName, string subProperty, string thirdProperty)
    {
        var subElement = GetSubProperty(userData, propertyName, subProperty);
        if (subElement is null)
        {
            return null;
        }

        var value = subElement.Value;
        if (value.ValueKind == JsonValueKind.Object
            && value.TryGetProperty(thirdProperty, out value))
        {
            return value.ToString();
        }

        return null;
    }
}

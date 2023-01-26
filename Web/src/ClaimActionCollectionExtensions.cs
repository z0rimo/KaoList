// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System.Security.Claims;

namespace CodeRabbits.KaoList.Web;

public static class ClaimActionCollectionExtensions
{
    /// <summary>
    /// Select a third level value from the json user data with the given top level key name, second level key name and third level third key name and add it as a Claim.
    /// This no-ops if the keys are not found or the value is empty.
    /// </summary>
    /// <param name="collection">The <see cref="ClaimActionCollection"/>.</param>
    /// <param name="claimType">The value to use for Claim.Type when creating a Claim.</param>
    /// <param name="jsonKey">The top level key to look for in the json user data.</param>
    /// <param name="subKey">The second level key to look for in the json user data.</param>
    /// <param name="thirdKey">The third level key to look for in the json user data.</param>
    public static void MapJsonThirdKey(this ClaimActionCollection collection, string claimType, string jsonKey, string subKey, string thirdKey)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        collection.MapJsonThirdKey(claimType, jsonKey, subKey, thirdKey, ClaimValueTypes.String);
    }

    /// <summary>
    /// Select a third level value from the json user data with the given top level key name, second level key name and third level third key name and add it as a Claim.
    /// This no-ops if the keys are not found or the value is empty.
    /// </summary>
    /// <param name="collection">The <see cref="ClaimActionCollection"/>.</param>
    /// <param name="claimType">The value to use for Claim.Type when creating a Claim.</param>
    /// <param name="jsonKey">The top level key to look for in the json user data.</param>
    /// <param name="subKey">The second level key to look for in the json user data.</param>
    /// <param name="thirdKey">The third level key to look for in the json user data.</param>
    /// <param name="valueType">The value to use for Claim.ValueType when creating a Claim.</param>
    public static void MapJsonThirdKey(this ClaimActionCollection collection, string claimType, string jsonKey, string subKey, string thirdKey, string valueType)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        collection.Add(new JsonThirdKeyClaimAction(claimType, valueType, jsonKey, thirdKey, subKey));
    }
}

// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CodeRabbits.Naver;

/// <summary>
/// Configuration options for <see cref="NaverHandler"/>.
/// </summary>
public class NaverOptions : OAuthOptions
{
    /// <summary>
    /// Initializes a new <see cref="NaverOptions"/>.
    /// </summary>
    public NaverOptions()
    {
        CallbackPath = new PathString("/signin-naver");
        AuthorizationEndpoint = NaverDefaults.AuthorizationEndpoint;
        TokenEndpoint = NaverDefaults.TokenEndpoint;
        UserInformationEndpoint = NaverDefaults.UserInformationEndpoint;

        ClaimActions.MapJsonSubKey(ClaimTypes.NameIdentifier, "response", "id");
    }
}
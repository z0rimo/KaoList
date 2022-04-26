// (c) 2022 CodeRabbits
// This code is licensed under MIT license (see LICENSE.txt for detail

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CodeRabbits.Kakao;

public class KakaoOptions : OAuthOptions
{
    /// <summary>
    /// Initializes a new <see cref="KakaoOptions"/>.
    /// </summary>
    public KakaoOptions()
    {
        CallbackPath = new PathString("/signin-kakao");
        AuthorizationEndpoint = KakaoDefaults.AuthorizationEndpoint;
        TokenEndpoint = KakaoDefaults.TokenEndpoint;
        UserInformationEndpoint = KakaoDefaults.UserInformationEndpoint;

        ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
    }    
}
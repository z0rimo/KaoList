// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.Naver;

/// <summary>
/// Default values for Naver authentication
/// </summary>
public static class NaverDefaults
{
    ///<summary>
    /// The default scheme for Naver authentication. Defaults to <c>Naver</c>.
    /// </summary>
    public const string AuthenticationScheme = "Naver";

    /// <summary>
    /// The default display name for Naver authentication. Defaults to <c>Naver</c>.
    /// </summary>
    public static readonly string DisplayName = "Naver";

    /// <summary>
    /// The default endpoint used to perform Naver authentication.
    /// </summary>
    public static readonly string AuthorizationEndpoint = "https://nid.naver.com/oauth2.0/authorize";

    /// <summary>
    /// The OAuth endpoint used to exchange access tokens.
    /// </summary>
    public static readonly string TokenEndpoint = "https://nid.naver.com/oauth2.0/token";

    /// <summary>
    /// The Naver endpoint that is used to gather additional user information.
    /// </summary>
    public static readonly string UserInformationEndpoint = "https://openapi.naver.com/v1/nid/me";
}

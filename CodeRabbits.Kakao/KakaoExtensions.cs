// (c) 2022 CodeRabbits
// This code is licensed under MIT license (see LICENSE.txt for detail

using CodeRabbits.Kakao;
using Microsoft.AspNetCore.Authentication;

namespace Microsoft.Extensions.DependencyInjection;

public static class KakaoExtensions
{
    /// <summary>
    /// Adds Kakao OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
    /// The default scheme is specified by <see cref="KakaoDefaults.AuthenticationScheme"/>.
    /// <para>
    /// Kakao authentication allows application users to sign in with their Kakao account.
    /// </para>
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
    /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
    public static AuthenticationBuilder AddKakao(this AuthenticationBuilder builder)
        => builder.AddKakao(KakaoDefaults.AuthenticationScheme, _ => { });

    /// <summary>
    /// Adds Kakao OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
    /// The default scheme is specified by <see cref="KakaoDefaults.AuthenticationScheme"/>.
    /// <para>
    /// Kakao authentication allows application users to sign in with their Kakao account.
    /// </para>
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
    /// <param name="configureOptions">A delegate to configure <see cref="KakaoOptions"/>.</param>
    /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
    public static AuthenticationBuilder AddKakao(this AuthenticationBuilder builder, Action<KakaoOptions> configureOptions)
        => builder.AddKakao(KakaoDefaults.AuthenticationScheme, configureOptions);

    /// <summary>
    /// Adds Kakao OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
    /// The default scheme is specified by <see cref="KakaoDefaults.AuthenticationScheme"/>.
    /// <para>
    /// Kakao authentication allows application users to sign in with their Kakao account.
    /// </para>
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
    /// <param name="authenticationScheme">The authentication scheme.</param>
    /// <param name="configureOptions">A delegate to configure <see cref="KakaoOptions"/>.</param>
    /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
    public static AuthenticationBuilder AddKakao(this AuthenticationBuilder builder, string authenticationScheme, Action<KakaoOptions> configureOptions)
        => builder.AddKakao(authenticationScheme, KakaoDefaults.DisplayName, configureOptions);

    /// <summary>
    /// Adds Kakao OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
    /// The default scheme is specified by <see cref="KakaoDefaults.AuthenticationScheme"/>.
    /// <para>
    /// Kakao authentication allows application users to sign in with their Kakao account.
    /// </para>
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
    /// <param name="authenticationScheme">The authentication scheme.</param>
    /// <param name="displayName">A display name for the authentication handler.</param>
    /// <param name="configureOptions">A delegate to configure <see cref="KakaoOptions"/>.</param>
    /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
    public static AuthenticationBuilder AddKakao(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<KakaoOptions> configureOptions)
        => builder.AddOAuth<KakaoOptions, KakaoHandler>(authenticationScheme, displayName, configureOptions);
}
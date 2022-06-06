// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

using CodeRabbits.Naver;
using Microsoft.AspNetCore.Authentication;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods to configure Naver OAuth authentication.
/// </summary>
public static class NaverExtensions
{
    /// <summary>
    /// Adds Naver OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
    /// The default scheme is specified by <see cref="NaverDefaults.AuthenticationScheme"/>.
    /// <para>
    /// Naver authentication allows application users to sign in with their Naver account.
    /// </para>
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
    /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
    public static AuthenticationBuilder AddNaver(this AuthenticationBuilder builder)
        => builder.AddNaver(NaverDefaults.AuthenticationScheme, _ => { });

    /// <summary>
    /// Adds Naver OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
    /// The default scheme is specified by <see cref="NaverDefaults.AuthenticationScheme"/>.
    /// <para>
    /// Naver authentication allows application users to sign in with their Naver account.
    /// </para>
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
    /// <param name="configureOptions">A delegate to configure <see cref="NaverOptions"/>.</param>
    /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
    public static AuthenticationBuilder AddNaver(this AuthenticationBuilder builder, Action<NaverOptions> configureOptions)
        => builder.AddNaver(NaverDefaults.AuthenticationScheme, configureOptions);

    /// <summary>
    /// Adds Naver OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
    /// The default scheme is specified by <see cref="NaverDefaults.AuthenticationScheme"/>.
    /// <para>
    /// Naver authentication allows application users to sign in with their Naver account.
    /// </para>
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
    /// <param name="authenticationScheme">The authentication scheme.</param>
    /// <param name="configureOptions">A delegate to configure <see cref="NaverOptions"/>.</param>
    /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
    public static AuthenticationBuilder AddNaver(this AuthenticationBuilder builder, string authenticationScheme, Action<NaverOptions> configureOptions)
        => builder.AddNaver(authenticationScheme, NaverDefaults.DisplayName, configureOptions);

    /// <summary>
    /// Adds Naver OAuth-based authentication to <see cref="AuthenticationBuilder"/> using the default scheme.
    /// The default scheme is specified by <see cref="NaverDefaults.AuthenticationScheme"/>.
    /// <para>
    /// Naver authentication allows application users to sign in with their Naver account.
    /// </para>
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
    /// <param name="authenticationScheme">The authentication scheme.</param>
    /// <param name="displayName">A display name for the authentication handler.</param>
    /// <param name="configureOptions">A delegate to configure <see cref="NaverOptions"/>.</param>
    /// <returns>A reference to <paramref name="builder"/> after the operation has completed.</returns>
    public static AuthenticationBuilder AddNaver(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<NaverOptions> configureOptions)
        => builder.AddOAuth<NaverOptions, NaverHandler>(authenticationScheme, displayName, configureOptions);
}
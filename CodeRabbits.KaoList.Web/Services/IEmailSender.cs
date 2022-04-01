// (c) 2021 CodeRabbits
// This code is licensed under MIT license (see LICENSE.txt for details)
//
// This document was created by referring to IEmailSender.cs of dotnet/aspnetcore.

namespace CodeRabbits.KaoList.Web.Services;

/// <summary>
///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
///     directly from your code. This API may change or be removed in future releases.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}

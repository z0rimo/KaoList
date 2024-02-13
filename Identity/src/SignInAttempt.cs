// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Net;

namespace CodeRabbits.KaoList.Identity;

/// <summary>
/// A log of login attempts.
/// </summary>
public class SignInAttempt
{
    /// <summary>
    /// Login Attempt Unique Number
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Login Attempted ID
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// The IP address from which the user is connecting
    /// </summary>
    public IPAddress? IpAddress { get; set; }

    /// <summary>
    /// The last modified date of the localized nickname.
    /// </summary>
    public DateTime? CreateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The attempted login was successful.
    /// </summary>
    public bool? Successed { get; set; }
}

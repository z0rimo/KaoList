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
    /// User's localized nickname.
    /// </summary>
    public IPAddress? IpAddress { get; set; }

    /// <summary>
    /// The last modified date of the localized nickname.
    /// </summary>
    public DateTime? CreateTime { get; set; } = DateTime.UtcNow;

    public bool? Successed { get; set; }
}

// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Identity;

/// <summary>
/// Provides localization of user nicknames
/// </summary>
public class KaoListUserLocalized
{
    /// <summary>
    /// Id of the user who owns the channel.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// The localization code name.
    /// </summary>
    public string? I18nName { get; set; }

    /// <summary>
    /// User's localized nickname.
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// The last modified date of the localized nickname.
    /// </summary>
    public DateTime? EditedDatetime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// A random value that must change whenever a user is persisted to the store
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}

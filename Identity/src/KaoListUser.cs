// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Identity;

namespace CodeRabbits.KaoList.Identity;

/// <inheritdoc />
public class KaoListUser : IdentityUser
{
    /// <summary>
    /// User's nickname
    /// </summary>
    public string? NickName { get; set; }


    /// <summary>
    /// This is a nick name with accents, uppercase and lowercase letters, katakana, width, and variations removed.    
    /// </summary>
    public string? NormalizedNickName { get; set; }

    /// <summary>
    /// The last modified date of the nickname.
    /// </summary>
    public DateTime? NickNameEditedDatetime { get; set; }

    /// <summary>
    /// Profile Icon uri path.
    /// </summary>
    public string? ProfileIcon { get; set; }

    /// <summary>
    /// Datetime the user was created.
    /// </summary>
    public DateTime? Created { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indicates whether the user has agreed to receive emails.
    /// </summary>
    public bool? AcceptEmail { get; set; } = false;
}

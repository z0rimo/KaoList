using Microsoft.AspNetCore.Identity;

namespace CodeRabbits.KaoList.Identity;

public class KaoListUser : KaoListUser<string>
{
}

/// <inheritdoc />
public class KaoListUser<TKey> : IdentityUser<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// User's nickname
    /// </summary>
    public string? NickName { get; set; }

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
}

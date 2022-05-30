namespace CodeRabbits.KaoList.Song;

/// <summary>
/// Blinded sing.
/// </summary>
public class SingBlind
{
    /// <summary>
    /// Blind sing id.
    /// </summary>
    public virtual string? SingId { get; set; }

    /// <summary>
    /// The user id who blined the sing.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// The blind date.
    /// </summary>
    public DateTime? Created { get; set; } = DateTime.UtcNow;
}
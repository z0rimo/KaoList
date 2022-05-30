namespace CodeRabbits.KaoList.Song;

/// <summary>
/// Blinded Instrumental.
/// </summary>
public class InstrumentalBlind
{
    /// <summary>
    /// Blind instrumental id.
    /// </summary>
    public virtual string? InstrumentalId { get; set; }

    /// <summary>
    /// The user id who blined the instrumental.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// The blind date.
    /// </summary>
    public DateTime? Created { get; set; } = DateTime.UtcNow;
}
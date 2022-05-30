namespace CodeRabbits.KaoList.Song;

/// <summary>
/// A user who followed instrumental.
/// </summary>
public class InstrumentalFollower
{
    /// <summary>
    /// The id of the followed instrumental.
    /// </summary>
    public virtual string? InstrumentalId { get; set; }

    /// <summary>
    /// The user id to follow.
    /// </summary>
    public virtual string? UserId { get; set; }

    /// <summary>
    /// The time the user followed this instrumental.
    /// </summary>
    public virtual DateTime? Created { get; set; } = DateTime.UtcNow;
}
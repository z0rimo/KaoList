namespace CodeRabbits.KaoList.Song;

/// <summary>
/// The user to followed the sing.
/// </summary>
public class SingFollower
{
    /// <summary>
    /// This is the sing to follow.
    /// </summary>
    public virtual string? SingId { get; set; }

    /// <summary>
    /// The id of the user who sing.
    /// </summary>
    public virtual string? UserId { get; set; }

    /// <summary>
    /// The date tiem you started following sing.
    /// </summary>
    public virtual DateTime? Created { get; set; }
}
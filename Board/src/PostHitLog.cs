namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Log of post hits.
/// </summary>
public class PostHitLog
{
    /// <summary>
    /// Id of the searched log.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Search word.
    /// </summary>
    public virtual int? PostId { get; set; }

    /// <summary>
    /// The time that post was searched.
    /// </summary>
    public virtual DateTime? CreateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Id of user who search the post.
    /// </summary>
    public virtual string? UserId { get; set; }

    /// <summary>
    /// A token to identify a user.
    /// </summary>
    public virtual string? IdentityToken { get; set; }
}

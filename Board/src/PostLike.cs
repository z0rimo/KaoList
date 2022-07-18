namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Description of likes applied to post.
/// </summary>
public class PostLike
{
    /// <summary>
    /// Id of PostLike.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Id of post that PostLike.
    /// </summary>
    public virtual int? PostId { get; set; }

    /// <summary>
    /// The time of applying PostLike to a post.
    /// </summary>
    public virtual DateTime? CreateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Id of user who applied PostLike.
    /// </summary>
    public virtual string? UserId { get; set; }

    /// <summary>
    /// Token to identify the user who applied PostLike.
    /// </summary>
    public virtual string? IdentityToken { get; set; }
}

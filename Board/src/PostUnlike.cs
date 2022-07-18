namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Unlikes on post.
/// </summary>
public class PostUnlike
{
    /// <summary>
    /// Id of PostUnlike.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Id of post that PostUnlike.
    /// </summary>
    public virtual int? PostId { get; set; }

    /// <summary>
    /// The time of applying PostUnlike to a post.
    /// </summary>
    public virtual DateTime? CreateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Id of user who applied PostUnlike.
    /// </summary>
    public virtual string? UserId { get; set; }

    /// <summary>
    /// Token to identify the user who applied PostUnlike.
    /// </summary>
    public virtual string? IdentityToken { get; set; }
}
  
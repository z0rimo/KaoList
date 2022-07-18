namespace CodeRabbits.KaoList.Board;

/// <summary>
/// User who wrote the post.
/// </summary>
public class PostUser
{
    /// <summary>
    /// Id of written post.
    /// </summary>
    public virtual int? PostId { get; set; }
    /// <summary>
    /// Id of user who written the post.
    /// </summary>
    public virtual string? UserId { get; set; }

}
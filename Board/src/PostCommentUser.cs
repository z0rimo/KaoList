namespace CodeRabbits.KaoList.Board;

/// <summary>
/// User who commented on post.
/// </summary>
public class PostCommentUser
{
    /// <summary>
    /// Id of comment written on the post.
    /// </summary>
    public virtual int? PostCommantId { get; set; }
    /// <summary>
    /// Id of user who commented on the post.
    /// </summary>
    public virtual string? UserId { get; set; }

}
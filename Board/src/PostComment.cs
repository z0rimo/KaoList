namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Comments on posts.
/// </summary>
public class PostComment
{
    /// <summary>
    /// Id of comment.
    /// </summary>
    public virtual int? Id { get; set; }

    /// <summary>
    /// Id of post with comment.
    /// </summary>
    public virtual int? PostId { get; set; }

    /// <summary>
    /// Original comment with replies.
    /// </summary>
    public virtual int? CommentParent { get; set; }

    /// <summary>
    /// Time the comment was written.
    /// </summary>
    public virtual DateTime? CreateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The content of comment.
    /// </summary>
    public virtual string? Comment { get; set; }

    /// <summary>
    /// The time the comment was requested to be deleted.
    /// </summary>
    public virtual DateTime? RemoveRequestTime { get; set; }

    /// <summary>
    /// A random value that must change whenever a user is persisted to the store.
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Status indicating whether a comment is blinded or not.
    /// </summary>
    public virtual bool? Blinded { get; set; }
}

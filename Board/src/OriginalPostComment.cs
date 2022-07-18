namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Original comment in the post written to community.
/// </summary>
public class OriginalPostComment
{
    /// <summary>
    /// Id of the comment in the post.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Id of original comment in the post written to community.
    /// </summary>
    public virtual int? PostCommentId { get; set; }

    /// <summary>
    /// Content of the comment before it was edited.
    /// </summary>
    public virtual string? Comment { get; set; }

    /// <summary>
    /// The time the original comment was posted before the comment was edited.
    /// </summary>
    public virtual DateTime? CreateTime { get; set; }
}
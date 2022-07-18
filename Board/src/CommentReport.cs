namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Reported Comments.
/// </summary>
public class CommentReport
{
    /// <summary>
    /// Id of the comment report.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Id of the reported comment.
    /// </summary>
    public virtual int? CommentId { get; set; }

    /// <summary>
    /// Time when the comment report was created.
    /// </summary>
    public virtual DateTime? CreateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Id of the user who reported the comment.
    /// </summary>
    public virtual string? UserId { get; set; }

    /// <summary>
    /// A token to identify the user who reported the comment.
    /// </summary>
    public virtual string? IdentityToken { get; set; }

    /// <summary>
    /// Content with the reason for reporting the comment.
    /// </summary>
    public virtual string? Content { get; set; }
}
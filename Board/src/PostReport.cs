namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Post that have been reported.
/// </summary>
public class PostReport
{
    /// <summary>
    /// Id of PostReport.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Id of reported post.
    /// 
    /// </summary>
    public virtual int? PostId { get; set; }

    /// <summary>
    /// The time the post was reported.
    /// </summary>
    public virtual DateTime? CreateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Id of user who reported the post.
    /// </summary>
    public virtual string? UserId { get; set; }

    /// <summary>
    /// A token to identify the user who reported the post.
    /// </summary>
    public virtual string? IdentityToken { get; set; }

    /// <summary>
    /// Contain the reason for report.
    /// </summary>
    public virtual string? Content { get; set; }

    /// <summary>
    /// Gets or sets the normalized content for this report of post.
    /// </summary>
    public virtual string? NormalizedContent { get; set; }
}

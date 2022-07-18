namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Original post written in community.
/// </summary>
public class OriginalPost
{
    /// <summary>
    /// Id of post written in community.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Id of original post written in community.
    /// </summary>
    public virtual int? PostId { get; set; }

    /// <summary>
    /// Title before the post was edited.
    /// </summary>
    public virtual string? Title { get; set; }

    /// <summary>
    /// Content before the post was edited.
    /// </summary>
    public virtual string? Content { get; set; }

    /// <summary>
    /// The time the post was created before the post was edited.
    /// </summary>
    public virtual DateTime? CreateTime { get; set; }
}
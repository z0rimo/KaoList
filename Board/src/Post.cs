namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Posts written to the community.
/// </summary>
public class Post
{
    /// <summary>
    /// Id of post written in community.
    /// </summary>
    public virtual int? Id { get; set; }

    /// <summary>
    /// The time that post was created.
    /// </summary>
    public virtual DateTime? CreateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Title of the post.
    /// </summary>
    public virtual string? Title { get; set; }

    /// <summary>
    /// Gets or sets the normalized title for this post.
    /// </summary>
    public virtual string? NormalizedTitle { get; set; }

    /// <summary>
    /// Contents of the post.
    /// </summary>
    public virtual string? Contents { get; set; }

    /// <summary>
    /// The time that post was requested to be deleted.
    /// </summary>
    public virtual int? RemoveRequestTime { get; set; }

    /// <summary>
    /// A random value that must change whenever a user is persisted to the store.
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Status indicating whether a post is blinded or not.
    /// </summary>
    public virtual bool? Blinded { get; set; }
}
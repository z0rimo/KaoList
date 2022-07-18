namespace CodeRabbits.KaoList.Board;

/// <summary>
/// This is description of head that separates post.
/// </summary>
public class Head
{
    /// <summary>
    /// Id of head.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The name of head to be displayed.
    /// </summary>
    public virtual string? DisplayName { get; set; }

    /// <summary>
    /// Normalized head.
    /// </summary>
    public virtual string? NomalizedDisplayName { get; set; }

    /// <summary>
    /// A random value that must change whenever a user is persisted to the store.
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; }
}

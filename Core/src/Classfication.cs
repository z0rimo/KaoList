namespace CodeRabbits.KaoList;

/// <summary>
/// Indicates the class to which the data belongs.
/// </summary>
public class Classfication
{
    /// <summary>
    /// A unique id for the classification.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The classification name to be displayed.
    /// </summary>
    public virtual string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the normalized display name for classfication.
    /// </summary>
    public virtual string? NormalizedDisplayName { get; set; }

    /// <summary>
    /// A random value that must change whenever a classfication is persisted to the store.
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}

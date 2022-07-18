namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Localized head.
/// </summary>
public class HeadLocalized
{
    /// <summary>
    /// Localized head id.
    /// </summary>
    public virtual string? HeadId { get; set; }

    /// <summary>
    /// Localized head name.
    /// </summary>
    public virtual string? I18nName { get; set; }

    /// <summary>
    /// Localized head names to be displayed on the site.
    /// </summary>
    public virtual string? Displayname { get; set; }

    /// <summary>
    /// A random value that must change whenever a user is persisted to the store.
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}

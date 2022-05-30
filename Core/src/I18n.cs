namespace CodeRabbits.KaoList;

/// <summary>
/// Localization language code.
/// </summary>
public class I18n
{
    /// <summary>
    /// Language name
    /// </summary>
    public virtual string? Name { get; set; }

    /// <summary>
    /// Normalized language name
    /// </summary>
    public virtual string? NormalizedName { get; set; }

    /// <summary>
    /// A random value that must change whenever a i18n is persisted to the store
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}

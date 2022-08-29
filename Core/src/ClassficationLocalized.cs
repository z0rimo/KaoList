namespace CodeRabbits.KaoList;

/// <summary>
/// The localized name of the class.
/// </summary>
public class ClassficationLocalized
{
    /// <summary>
    /// The id of the localized classification.
    /// </summary>
    public virtual string? ClassficationId { get; set; }

    /// <summary>
    /// Localized language code.
    /// </summary>
    public virtual string? I18nName { get; set; }

    /// <summary>
    /// Localized display name.
    /// </summary>
    public virtual string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the normalized display name for classfication localized.
    /// </summary>
    public virtual string? NormalizedDisplayName { get; set; }

    /// <summary>
    /// A random value that must change whenever a classfication localized is persisted to the store
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}

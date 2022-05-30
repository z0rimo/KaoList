namespace CodeRabbits.KaoList.Song;

/// <summary>
/// The localized name of instrumental.
/// </summary>
public class InstrumentalLocalized
{
    /// <summary>
    /// localized instrumental id.
    /// </summary>
    public virtual string? InstrumentalId { get; set; }

    /// <summary>
    /// localized language name.
    /// </summary>
    public virtual string? I18nName { get; set; }

    /// <summary>
    /// The localized title of the song.
    /// </summary>   
    public virtual string? Title { get; set; }

    /// <summary>
    /// A random value that must change whenever a song localized is persisted to the store.
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}
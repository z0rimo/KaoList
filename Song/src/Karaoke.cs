namespace CodeRabbits.KaoList.Song;

/// <summary>
/// The sing number registered in karaoke.
/// </summary>
public class Karaoke
{
    /// <summary>
    /// The brand name of the karaoke provider.
    /// </summary>
    public virtual string? Provider { get; set; }

    /// <summary>
    /// This is the registered sing no of that provider.
    /// </summary>
    public virtual string? No { get; set; }

    /// <summary>
    /// This is the Instrumental id that the song no means.
    /// </summary>
    public virtual string? InstrumentalId { get; set; }

    /// <summary>
    /// The name to be displayed.
    /// </summary>
    public virtual string? DisplayName { get; set; }

    /// <summary>
    /// The created is added date for karaoke
    /// </summary>
    public virtual DateTime? Created { get; set; }

    /// <summary>
    /// A random value that must change whenever a karaoke is persisted to the store.
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}
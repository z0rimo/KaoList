namespace CodeRabbits.KaoList.Song;

/// <summary>
/// This is the instrumental of the song.
/// </summary>
public class Instrumental
{
    /// <summary>
    /// This is a unique instrumental id.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The title of the instrumental.
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// This is a Title with accents, uppercase and lowercase letters, katakana, width, and variations removed.    
    /// </summary>
    public string? NormalizedTitle { get; set; }

    /// <summary>
    /// The sound id of the instrumental.
    /// </summary>
    public string? SoundId { get; set; }

    ///<summary>
    /// The composer of the instrumental
    ///<summary>
    public string? Composer { get; set; }

    /// <summary>
    /// The time the instrumental was created.
    /// </summary>
    public DateTime? Created { get; set; }

    /// <summary>
    /// A random value that must change whenever a instrumental is persisted to the store.
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}
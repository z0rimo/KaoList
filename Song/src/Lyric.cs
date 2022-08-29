namespace CodeRabbits.KaoList.Song;

/// <summary>
/// These are the lyrics of the instrumental.
/// </summary>
public class Lyric
{
    /// <summary>
    /// It is a instrumental that corresponds to the lyrics.
    /// </summary>
    public virtual string? InstrumentalId { get; set; }

    /// <summary>
    /// An ordered sequence of lyrics.
    /// </summary>
    public virtual int? Sequence { get; set; }

    /// <summary>
    /// It's lyrics start time.
    /// </summary>
    public TimeSpan? Offset { get; set; }

    /// <summary>
    /// The duration of the lyrics.
    /// </summary>
    public TimeSpan? Duration { get; set; }

    /// <summary>
    /// It is the content of the lyrics corresponding to the time.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Normalized Lyrics Content.
    /// </summary>
    public string? NormalizedContent { get; set; }
}
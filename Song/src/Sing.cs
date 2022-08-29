namespace CodeRabbits.KaoList.Song;

/// <summary>
/// The sing the song corresponding to instrumental.
/// </summary>
public class Sing
{
    /// <summary>
    /// A unique id for the sing.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// This is the instrumental id of the sing.
    /// </summary>
    public virtual string? InstrumentalId { get; set; }

    /// <summary>
    /// The language code of the sing.
    /// </summary>
    public virtual string? Language { get; set; }

    /// <summary>
    /// The sound id of the sing.
    /// </summary>
    public string? SoundId { get; set; }

    /// <summary>
    /// The datetime the sing was created.
    /// </summary>
    public virtual DateTime? Created { get; set; } = DateTime.UtcNow;

}
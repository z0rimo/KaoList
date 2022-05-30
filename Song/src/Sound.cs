namespace CodeRabbits.KaoList.Song;

/// <summary>
/// Sound information that can play the song.
/// </summary>
public class Sound
{
    /// <summary>
    /// The unique id of the sound.
    /// </summary>
    public virtual string? Id { get; set; }

    /// <summary>
    /// A path through which the sound can be played.
    /// </summary>
    public virtual string? Path { get; set; }
}
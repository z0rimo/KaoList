namespace CodeRabbits.KaoList.Song;

/// <summary>
/// This is a log that play the sound.
/// </summary>
public class SoundPlaylogDetail
{
    /// <summary>
    /// Sound play unique log id.
    /// </summary>
    public virtual string? Id { get; set; }

    /// <summary>
    /// Played sound id.
    /// </summary>
    public virtual string? SoundPlayLogId { get; set; }

    /// <summary>
    /// The time the event log occurred.
    /// </summary>
    public virtual TimeSpan? CurrentTime { get; set; }

    /// <summary>
    /// Play status.
    /// </summary>
    public virtual PlayState? Status { get; set; }

    /// <summary>
    /// The time the event log occurred.
    /// </summary>
    public virtual DateTime? Created { get; set; } = DateTime.UtcNow;

}
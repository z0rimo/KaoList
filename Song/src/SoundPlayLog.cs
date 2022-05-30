namespace CodeRabbits.KaoList.Song;

/// <summary>
/// This is a log that play the sound.
/// </summary>
public class SoundPlayLog
{
    /// <summary>
    /// Sound play unique log id.
    /// </summary>
    public virtual string? Id { get; set; }

    /// <summary>
    /// Played sound id.
    /// </summary>
    public virtual string? SoundId { get; set; }

    /// <summary>
    /// The id of the user who sing.
    /// </summary>
    public virtual string? UserId { get; set; }

    /// <summary>
    /// A token to identify the user who played it.
    /// </summary>
    public virtual string? IdentityToken { get; set; }

    /// <summary>
    /// It is the date of renewal.
    /// </summary>
    public virtual DateTime? Created { get; set; } = DateTime.UtcNow;

}
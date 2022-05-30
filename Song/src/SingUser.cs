namespace CodeRabbits.KaoList.Song;

/// <summary>
/// The user who sang the song.
/// </summary>
public class SingUser
{
    /// <summary>
    /// The id of the sing.
    /// </summary>
    public virtual string? SingId { get; set; }

    /// <summary>
    /// The id of the user who sing.
    /// </summary>
    public virtual string? UserId { get; set; }
}
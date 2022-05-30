namespace CodeRabbits.KaoList.Song;

/// <summary>
/// A title song among specific songs sung by the user.
/// </summary>
public class TitleSing
{
    /// <summary>
    /// The id of the sing to title.
    /// </summary>
    public virtual string? SingId { get; set; }

    /// <summary>
    /// The instrumenta id of the sing.
    /// 
    /// It is put in for the unique constraint.
    /// </summary>
    public virtual string? InstrumentalId { get; set; }

    /// <summary>
    /// The id of the user who sing.
    /// 
    /// It is put in for the unique constraint.
    /// </summary>
    public virtual string? UserId { get; set; }

}
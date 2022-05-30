namespace CodeRabbits.KaoList.Song;

/// <summary>
/// Popular song information.
/// </summary>
public class PopularSing
{
    /// <summary>
    /// The id of a popular sing.
    /// </summary>
    public virtual string? SingId { get; set; }

    /// <summary>
    /// Popularity score.
    /// </summary>
    public virtual double? Score { get; set; }

    /// <summary>
    /// The time the score was scored.
    /// </summary>
    public virtual DateTime? Created { get; set; }
}

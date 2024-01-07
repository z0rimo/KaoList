namespace CodeRabbits.KaoList.Song;

/// <summary>
/// Popular song information.
/// </summary>
public class PopularDailySing
{
    /// <summary>
    /// The id of a daily popular sing.
    /// </summary>
    public virtual string? SingId { get; set; }

    /// <summary>
    /// Popularity score.
    /// </summary>
    public virtual double? Score { get; set; }

    /// <summary>
    /// The time the score was scored.
    /// </summary>
    public virtual DateTime? Created { get; set; } = DateTime.Now;
}

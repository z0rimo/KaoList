namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Chart voting.
/// </summary>
public class PostChartVote
{
    /// <summary>
    /// Id of the vote.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Id of the voted item.
    /// </summary>
    public virtual string? PostChartItemId { get; set; }

    /// <summary>
    /// The time user voted.
    /// </summary>
    public virtual DateTime? CreateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// A token to identify the user who voted.
    /// </summary>
    public virtual string? IdentityToken { get; set; }
}

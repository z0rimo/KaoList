namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Chart voting rules.
/// </summary>
public class PostChartVoteRole
{
    /// <summary>
    /// Id of voting role.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The name of the voting role.
    /// </summary>
    public virtual string? Name { get; set; }

    /// <summary>
    /// The name of the nomalized voting role.
    /// </summary>
    public virtual string? NormalizedName { get; set; }

    /// <summary>
    /// A random value that must change whenever a user is persisted to the store.
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}
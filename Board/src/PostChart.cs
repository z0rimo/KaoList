namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Chart wrtten in the post.
/// </summary>
public class PostChart
{
    /// <summary>
    /// Id of chart included in post.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Id of post containing chart.
    /// </summary>
    public virtual int? PostId { get; set; }

    /// <summary>
    /// The title of the chart.
    /// </summary>
    public virtual string? Title { get; set; }

    /// <summary>
    /// Gets or sets the normalized title for this chart of post.
    /// </summary>
    public virtual string? NormalizedTitle { get; internal set; }

    /// <summary>
    /// Number of items to choose from in the chart.
    /// </summary>
    public virtual byte? VoteNumber { get; set; }

    /// <summary>
    /// Date the chart voting ends.
    /// </summary>
    public virtual DateTime? EndDateTime { get; set; }

    /// <summary>
    /// Rule for determining who can vote.
    /// </summary>
    public virtual string? VoteRole { get; set; }
}
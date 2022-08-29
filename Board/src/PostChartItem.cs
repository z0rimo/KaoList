namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Items in the chart.
/// </summary>
public class PostChartItem
{
    /// <summary>
    /// Id of the item to be in chart.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Id of chart included in post.
    /// </summary>
    public virtual string? PostChartId { get; set; }

    /// <summary>
    /// Title of each item in the chart.
    /// </summary>
    public virtual string? Title { get; set; }

    /// <summary>
    /// Gets or sets the normalized title for item of post chart.
    /// </summary>
    public virtual string? NormalizedTitle { get; set; }
}

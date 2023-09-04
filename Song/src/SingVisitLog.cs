namespace CodeRabbits.KaoList.Song;
/// <summary>
/// A log of visits to Sing's detail page.
/// </summary>
public class SingVisitLog
{
    /// <summary>
    /// A unique id in the visit log.
    /// </summary>
    public virtual string? Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The unique id of the sing.
    /// </summary>
    public virtual string? SingId { get; set; }

    /// <summary>
    /// The date the log was created.
    /// </summary>
    public DateTime? Created { get; set; } = DateTime.UtcNow;
}

namespace CodeRabbits.KaoList;

public class AppLog
{
    /// <summary>
    /// Log unique identification key
    /// </summary>
    public virtual int? Id { get; set; }

    /// <summary>
    /// The time the log was created.
    /// </summary>
    public virtual DateTime? Created { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Log contents
    /// </summary>
    public virtual string? Log { get; set; }
}

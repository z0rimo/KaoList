namespace CodeRabbits.KaoList.Song;

/// <summary>
/// This is a instrumental classification.
/// </summary>
public class InstrumentalClassification
{
    /// <summary>
    /// Classified id of instrumental.
    /// </summary>
    public virtual string? InstrumentalId { get; set; }

    /// <summary>
    /// The id of the classified item.
    /// </summary>
    public virtual string? ClassficationId { get; set; }
}
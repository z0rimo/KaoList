namespace CodeRabbits.KaoList.Board;

/// <summary>
/// Head stands for post head.
/// </summary>
public class PostHead
{
    /// <summary>
    /// ID of post that contains head.
    /// </summary>
    public virtual int? PostId { get; set; }

    /// <summary>
    /// Id of head applied to post.
    /// </summary>
    public virtual string? HeadId { get; set; }
}
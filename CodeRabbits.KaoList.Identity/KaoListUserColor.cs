namespace CodeRabbits.KaoList.Identity;

/// <summary>
/// The color the user used in the editor
/// </summary>
public class KaoListUserColor
{
    /// <summary>
    /// A color value expressed in 32 bits.
    /// </summary>
    public int? Color { get; set; }

    /// <summary>
    /// The last modified date of the nickname.
    /// </summary>
    public string? UserId { get; set; }


    /// <summary>
    /// The date the user used the color.
    /// </summary>
    public DateTime? CreateTime { get; set; } = DateTime.UtcNow;
}

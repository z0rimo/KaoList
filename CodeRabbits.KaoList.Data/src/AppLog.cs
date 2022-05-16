using System.ComponentModel.DataAnnotations;

namespace CodeRabbits.KaoList.Data;

public class AppLog
{
    /// <summary>
    /// Log unique identification key
    /// </summary>
    [Key]
    public int? Id { get; set; }

    /// <summary>
    /// The time the log was created.
    /// </summary>
    [Required]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Log contents
    /// </summary>
    [Required]
    public string? Log { get; set; }
}

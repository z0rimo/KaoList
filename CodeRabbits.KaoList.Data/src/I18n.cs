using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CodeRabbits.KaoList.Data;

[Index(nameof(NormalizedName), IsUnique = true)]
public class I18n
{
    /// <summary>
    /// Language name
    /// </summary>
    [Key]
    [MaxLength(50)]
    public string? Name { get; set; }

    /// <summary>
    /// Normalized language name
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string? NormalizedName { get; set; }

    /// <summary>
    /// A random value that must change whenever a user is persisted to the store
    /// </summary>
    [Required]
    [ConcurrencyCheck]
    public string? ConcurrencyStamp { get; set; }
}

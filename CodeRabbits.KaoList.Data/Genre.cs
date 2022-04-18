using System.ComponentModel.DataAnnotations;

namespace CodeRabbits.KaoList.Data;

public class Genre
{
    public int? Id { get; set; }
    [MaxLength(300)]
    public string? Descriptopn { get; set; }
    public virtual List<SongGenre>? SongGenres { get; set; }
}

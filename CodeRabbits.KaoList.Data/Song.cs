using System.ComponentModel.DataAnnotations;

namespace CodeRabbits.KaoList.Data;

public class Song
{
    public int? Id { get; set; }
    [MaxLength(300)]
    public string? Title { get; set; }
    [MaxLength(300)]
    public string? Artist { get; set; }
    [MaxLength(20)]
    public string? YouTubeId { get; set; }
    public DateTime CreateDate { get; set; }

    public virtual Ky? Ky { get; set; }
    public virtual Tj? Tj { get; set; }
    public virtual List<SongGenre>? SongGenres { get; set; }
    public virtual List<Bookmark>? Bookmarks { get; set; }

}

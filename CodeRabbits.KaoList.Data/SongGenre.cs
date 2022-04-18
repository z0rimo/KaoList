namespace CodeRabbits.KaoList.Data;

public class SongGenre
{
    public int? SongId { get; set; }
    public int? SongGenreId { get; set; }

    public virtual Song? Song { get; set; }
    public virtual Genre? Genre { get; set; }    
}

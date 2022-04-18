namespace CodeRabbits.KaoList.Data;

public class Ky
{
    public int? No { get; set; }
    public int? SongId { get; set; }
    public DateTime CreateDate { get; set; }
    public virtual Song? Songs { get; set; }
}

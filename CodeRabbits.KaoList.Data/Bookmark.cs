using CodeRabbits.KaoList.Bookmark;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CodeRabbits.KaoList.Data;

public class Bookmark : UserBookmark
{
    public override sealed int SongId { get; set; } = default!;

    [MaxLength(450)]
    public override sealed string UserId { get; set; } = default!;
    public DateTime CreateDate { get; set; }
    public virtual KaoListUser? User { get; set; }
    public virtual Song? Song { get; set; }
}

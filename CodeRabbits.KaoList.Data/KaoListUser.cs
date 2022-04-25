using Microsoft.AspNetCore.Identity;

namespace CodeRabbits.KaoList.Data
{
    public class KaoListUser : IdentityUser
    {
        public string? NickName { get; set; }
        public virtual IEnumerable<Bookmark>? Bookmarks { get; set; }
    }
}

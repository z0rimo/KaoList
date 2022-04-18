using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRabbits.KaoList.Data
{
    public class KaoListUser : IdentityUser
    {
        public string? NickName { get; set; }
        public virtual IEnumerable<Bookmark>? Bookmarks { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeRabbits.KaoList.Web.Data
{
    public class KaoListDbContext : IdentityDbContext
    {        
        public KaoListDbContext(DbContextOptions<KaoListDbContext> options)
            : base(options)
        {
        }
    }
}
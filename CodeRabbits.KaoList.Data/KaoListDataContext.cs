using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeRabbits.KaoList.Data;

public class KaoListDataContext : IdentityDbContext<KaoListUser>
{
    public KaoListDataContext(DbContextOptions options)
        : base(options)
    {
    }    


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

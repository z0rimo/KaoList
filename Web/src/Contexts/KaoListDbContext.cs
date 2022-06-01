using CodeRabbits.KaoList.Data;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CodeRabbits.KaoList.Web.Data
{
    public class KaoListDbContext : KaoListDataContext
    {
        public KaoListDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {           
        }
    }
}
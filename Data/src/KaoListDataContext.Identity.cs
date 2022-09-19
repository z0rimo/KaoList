using CodeRabbits.KaoList.Identity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;

namespace CodeRabbits.KaoList.Data;

public partial class KaoListDataContext<TUser> : ApiAuthorizationDbContext<TUser> where TUser : KaoListUser
{
    public virtual DbSet<KaoListUserBlind> UserBlinds { get; set; } = default!;
    public virtual DbSet<KaoListUserChannel> UserChannels { get; set; } = default!;
    public virtual DbSet<KaoListUserColor> UserColors { get; set; } = default!;
    public virtual DbSet<KaoListUserDeleteReason> UserDeleteReason { get; set; } = default!;
    public virtual DbSet<KaoListUserFollower> UserFollower { get; set; } = default!;
    public virtual DbSet<KaoListUserLanguage> UserLanguage { get; set; } = default!;
    public virtual DbSet<KaoListUserLocalized> UserLocalized { get; set; } = default!;
}

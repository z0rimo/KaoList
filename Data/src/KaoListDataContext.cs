using CodeRabbits.KaoList.Board;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Song;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using CodeRabbits.KaoList.Playlist;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CodeRabbits.KaoList.Data;

public partial class KaoListDataContext : KaoListDataContext<KaoListUser> {
    public KaoListDataContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
    : base(options, operationalStoreOptions)
    {

    }
}

public partial class KaoListDataContext<TUser> : ApiAuthorizationDbContext<TUser> where TUser : KaoListUser
{
    public virtual DbSet<AppLog> AppLogs { get; set; } = default!;
    public virtual DbSet<I18n> I18ns { get; set; } = default!;
    public virtual DbSet<Classfication> Classfications { get; set; } = default!;
    public virtual DbSet<ClassficationLocalized> ClassficationLocalizeds { get; set; } = default!;
    public virtual DbSet<Instrumental> Instrumental { get; set; } = default!;
    public virtual DbSet<InstrumentalBlind> InstrumentalBlinds { get; set; } = default!;
    public virtual DbSet<InstrumentalClassification> InstrumentalClassifications { get; set; } = default!;
    public virtual DbSet<InstrumentalFollower> InstrumentalFollowers { get; set; } = default!;
    public virtual DbSet<InstrumentalLocalized> InstrumentalLocalizeds { get; set; } = default!;
    public virtual DbSet<Lyric> Lyrics { get; set; } = default!;
    public virtual DbSet<PopularSing> PopularSings { get; set; } = default!;
    public virtual DbSet<SignInAttempt> SignInAttempts { get; set; } = default!;
    public virtual DbSet<Sing> Sings { get; set; } = default!;
    public virtual DbSet<SingUser> SingUsers { get; set; } = default!;
    public virtual DbSet<SingBlind> SingBlinds { get; set; } = default!;
    public virtual DbSet<SingFollower> SingFollowers { get; set; } = default!;
    public virtual DbSet<SongSearchLog> SongSearchLogs { get; set; } = default!;
    public virtual DbSet<SoundPlayLog> SoundPlayLogs { get; set; } = default!;
    public virtual DbSet<Sound> Sounds { get; set; } = default!;
    public virtual DbSet<TitleSing> TitleSings { get; set; } = default!;
    public virtual DbSet<Karaoke> Karaokes { get; set; } = default!;

    public KaoListDataContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
    : base(options, operationalStoreOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.BoardEntitiesBuild<KaoListUser>();
        builder.KaoListEntitiesBuild();
        builder.SongEntitiesBuild<KaoListUser>();
        builder.KaoListPlaylistEntityBuilder<KaoListUser>();
        builder.KaoListIdentityEntitiesBuild<KaoListUser>();
    }
}

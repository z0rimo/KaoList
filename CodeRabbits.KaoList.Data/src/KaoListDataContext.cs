using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Song;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net;

namespace CodeRabbits.KaoList.Data;

public class KaoListDataContext : KaoListDataContext<KaoListUser>
{
    public KaoListDataContext(DbContextOptions options) : base(options) { }
    protected KaoListDataContext() { }
}

public class KaoListDataContext<TUser> : IdentityDbContext<TUser, IdentityRole, string> where TUser : KaoListUser
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
    public virtual DbSet<KaoListUserBlind> UserBlinds { get; set; } = default!;
    public virtual DbSet<KaoListUserChannel> UserChannels { get; set; } = default!;
    public virtual DbSet<KaoListUserColor> UserColors { get; set; } = default!;
    public virtual DbSet<KaoListUserDeleteReason> UserDeleteReason { get; set; } = default!;
    public virtual DbSet<KaoListUserFollower> UserFollower { get; set; } = default!;
    public virtual DbSet<KaoListUserLanguage> UserLanguage { get; set; } = default!;
    public virtual DbSet<KaoListUserLocalized> UserLocalized { get; set; } = default!;
    public virtual DbSet<Lyric> Lyrics { get; set; } = default!;
    public virtual DbSet<PopularSing> PopularSings { get; set; } = default!;
    public virtual DbSet<SignInAttempt> SignInAttempts { get; set; } = default!;
    public virtual DbSet<Sing> Sings { get; set; } = default!;
    public virtual DbSet<SingUser> SingUsers { get; set; } = default!;
    public virtual DbSet<SongSearchLog> SongSearchLogs { get; set; } = default!;
    public virtual DbSet<SoundPlayLog> SoundPlayLogs { get; set; } = default!;
    public virtual DbSet<Sound> Sounds { get; set; } = default!;
    public virtual DbSet<TitleSing> TitleSings { get; set; } = default!;

    public KaoListDataContext(DbContextOptions options) : base(options) { }

    protected KaoListDataContext() { }

    private sealed class IPAddressConverter : ValueConverter<IPAddress?, byte[]?>
    {
        public IPAddressConverter() : base(i => IpAddressToBytes(i), b => BytesToIpAddress(b)) { }

        private static byte[]? IpAddressToBytes(IPAddress? ipAddress) => ipAddress?.GetAddressBytes() ?? null;
        private static IPAddress? BytesToIpAddress(byte[]? bytes) => bytes is not null ? new IPAddress(bytes) : null;
    }

    private static void IdentityEntitiesBuild<TKey>(ModelBuilder builder) where TKey : IEquatable<TKey>
    {
        builder.Entity<I18n>(b =>
        {
            b.HasMany<KaoListUserLocalized>().WithOne().HasForeignKey(ul => ul.i18nName).IsRequired();
            b.HasMany<KaoListUserLanguage>().WithOne().HasForeignKey(u => u.I18nName).IsRequired();
        });

        builder.Entity<IdentityRole>(b =>
        {
            b.ToTable("KaoListRoles");
        });

        builder.Entity<IdentityRoleClaim<TKey>>(b =>
        {
            b.ToTable("KaoListClaims");
        });

        builder.Entity<IdentityUserClaim<TKey>>(b =>
        {
            b.ToTable("KaoListUserClaims");
        });

        builder.Entity<IdentityUserLogin<TKey>>(b =>
        {
            b.ToTable("KaoListUserLogins");
        });

        builder.Entity<IdentityUserRole<TKey>>(b =>
        {
            b.ToTable("KaoListUserRoles");
        });

        builder.Entity<IdentityUserToken<TKey>>(b =>
        {
            b.ToTable("KaoListUserTokens");
        });

        builder.Entity<TUser>(b =>
        {
            b.HasIndex(u => u.NickName).HasDatabaseName("UserNickNameIndex");
            b.ToTable("KaoListUsers");

            b.Property(u => u.NickName).HasMaxLength(256);
            b.Property(u => u.Created).IsRequired();

            b.HasMany<KaoListUserBlind>().WithOne().HasForeignKey(ub => ub.BlinedUserId).IsRequired();
            // Since OnDelete action is performed in the preceding BlinedUserId,
            // The following UserId does not perform any action.
            b.HasMany<KaoListUserBlind>().WithOne().HasForeignKey(ub => ub.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            b.HasMany<KaoListUserChannel>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            b.HasMany<KaoListUserColor>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            b.HasMany<KaoListUserFollower>().WithOne().HasForeignKey(uf => uf.FollowUserId).IsRequired();
            // Since OnDelete action is performed in the preceding FollowUserId,
            // The following FollwerUserId does not perform any action.
            b.HasMany<KaoListUserFollower>().WithOne().HasForeignKey(uf => uf.FollwerUserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            b.HasMany<KaoListUserLanguage>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            b.HasMany<KaoListUserLocalized>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            b.HasMany<SignInAttempt>().WithOne().HasForeignKey(sa => sa.UserId).IsRequired();
        });

        builder.Entity<KaoListUserBlind>(b =>
        {
            b.HasKey(u => new { u.UserId, u.BlinedUserId });
            b.ToTable("KaoListUserBlinds");

            b.Property(u => u.CreateTime).IsRequired();
        });


        builder.Entity<KaoListUserChannel>(b =>
        {
            b.HasKey(uc => new { uc.ChannelProvider, uc.ProviderKey });
            b.ToTable("KaoListUserChannles");

            b.Property(uc => uc.ChannelProvider).HasMaxLength(256);
            b.Property(uc => uc.ProviderKey).HasMaxLength(256);
        });

        builder.Entity<KaoListUserColor>(b =>
        {
            b.HasKey(uc => new { uc.Color, uc.UserId });
            b.ToTable("KaoListUserColors");

            b.Property(u => u.CreateTime).IsRequired();
        });

        builder.Entity<KaoListUserDeleteReason>(b =>
        {
            b.HasKey(dr => dr.Id);
            b.ToTable("KaoListUserDeleteReasons");

            b.Property(sa => sa.Id)
             .ValueGeneratedOnAdd()
             .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            b.Property(u => u.CreateTime).IsRequired();
        });

        builder.Entity<KaoListUserFollower>(b =>
        {
            b.HasKey(uf => new { uf.FollowUserId, uf.FollwerUserId });
            b.ToTable("KaoListUserFollowers");

            b.Property(u => u.CreateTime).IsRequired();
        });

        builder.Entity<KaoListUserLanguage>(b =>
        {
            b.ToTable("KaoListUserLanguages")
            ;
            b.HasKey(ul => new { ul.I18nName, ul.UserId });
        });


        builder.Entity<KaoListUserLocalized>(b =>
        {
            b.HasKey(ul => new { ul.i18nName, ul.UserId });
            b.ToTable("KaoListUserLocalizeds");
            b.Property(ul => ul.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(u => u.EditedDatetime).IsRequired();
        });

        builder.Entity<SignInAttempt>(b =>
        {
            b.HasKey(sa => sa.Id);
            b.ToTable("SignInAttempts");

            b.Property(sa => sa.Id)
             .ValueGeneratedOnAdd()
             .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            b.Property(sa => sa.IpAddress).HasConversion<IPAddressConverter>();
            b.Property(sa => sa.CreateTime).IsRequired();
            b.Property(sa => sa.Successed).IsRequired();
        });
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.KaoListEntitiesBuild();
        builder.SongEntitiesBuild<KaoListUser>();
        IdentityEntitiesBuild<string>(builder);
    }
}

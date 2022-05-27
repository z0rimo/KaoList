using CodeRabbits.KaoList.Identity;
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
    public virtual DbSet<KaoListUserBlind> UserBlinds { get; set; } = default!;
    public virtual DbSet<KaoListUserChannle> UserChannles { get; set; } = default!;
    public virtual DbSet<KaoListUserColor> UserColors { get; set; } = default!;
    public virtual DbSet<KaoListUserDeleteReason> UserDeleteReason { get; set; } = default!;
    public virtual DbSet<KaoListUserFollower> UserFollower { get; set; } = default!;
    public virtual DbSet<KaoListUserLocalized> UserLocalized { get; set; } = default!;
    public virtual DbSet<SignInAttempt> SignInAttempts { get; set; } = default!;

    public KaoListDataContext(DbContextOptions options) : base(options) { }

    protected KaoListDataContext() { }

    private sealed class IPAddressConverter : ValueConverter<IPAddress?, byte[]?>
    {
        public IPAddressConverter() : base(i => IpAddressToBytes(i), b => BytesToIpAddress(b)) { }

        private static byte[]? IpAddressToBytes(IPAddress? ipAddress) => ipAddress?.GetAddressBytes() ?? null;
        private static IPAddress? BytesToIpAddress(byte[]? bytes) => bytes is not null ? new IPAddress(bytes) : null;
    }

    public static void CommonEntitiesBuild(ModelBuilder builder)
    {
        builder.Entity<AppLog>(b =>
        {
            b.Property(p => p.Id)
             .ValueGeneratedOnAdd()
             .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            b.Property(p => p.CreateTime)
             .HasColumnType("datetime2")
             .HasDefaultValueSql("(GETUTCDATE())")
             .IsRequired();

            b.Property(p => p.Log)
             .HasColumnType("nvarchar(max)")
             .IsRequired();

            b.HasKey(p => p.Id);
        });

        builder.Entity<I18n>(b =>
        {
            b.HasKey(p => p.Name);
            b.HasIndex(p => p.NormalizedName).IsUnique();

            b.Property(p => p.Name).HasColumnType("nvarchar(50)").IsUnicode(false);
            b.Property(p => p.NormalizedName).HasColumnType("nvarchar(50)").IsRequired().IsUnicode(false);
            b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken().HasColumnType("nvarchar(max)").IsRequired();

            b.HasMany<TUser>().WithOne().HasForeignKey(u => u.DefaultLanguage).IsRequired();
        });
    }


    public static void IdentityEntitiesBuild<TKey>(ModelBuilder builder) where TKey : IEquatable<TKey>
    {
        IPAddressConverter converter = new();

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
            b.Property(u => u.CreateTime).HasDefaultValueSql("(GETUTCDATE())").IsRequired();
            b.Property(u => u.DefaultLanguage).HasMaxLength(50);

            b.HasMany<KaoListUserBlind>().WithOne().HasForeignKey(ub => ub.BlinedUserId).IsRequired();
            b.HasMany<KaoListUserBlind>().WithOne().HasForeignKey(ub => ub.UserId).IsRequired();
            b.HasMany<KaoListUserChannle>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            b.HasMany<KaoListUserColor>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            b.HasMany<KaoListUserFollower>().WithOne().HasForeignKey(uf => uf.FollwerUserId).IsRequired();
            b.HasMany<KaoListUserFollower>().WithOne().HasForeignKey(uf => uf.FollowUserId).IsRequired();
            b.HasMany<KaoListUserLocalized>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            b.HasMany<SignInAttempt>().WithOne().HasForeignKey(sa => sa.UserId).IsRequired();
        });

        builder.Entity<KaoListUserBlind>(b =>
        {
            b.HasKey(u => new { u.UserId, u.BlinedUserId });

            b.Property(u => u.CreateTime).IsRequired();
        });


        builder.Entity<KaoListUserChannle>(b =>
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
            b.Property(sa => sa.IpAddress).HasConversion(converter);
            b.Property(sa => sa.CreateTime).IsRequired();
            b.Property(sa => sa.Successed).IsRequired();
        });
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        CommonEntitiesBuild(builder);
        IdentityEntitiesBuild<string>(builder);
    }
}

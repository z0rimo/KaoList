using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeRabbits.KaoList.Data;

public class KaoListDataContext : IdentityDbContext<KaoListUser>
{
    public virtual DbSet<AppLog> AppLogs { get; set; } = default!;
    public virtual DbSet<I18n> I18ns { get; set; } = default!;

    public KaoListDataContext(DbContextOptions options)
        : base(options)
    {
    }

    public static void CommonEntitiesBuild(ModelBuilder builder)
    {
        builder.Entity<AppLog>(entitiy =>
        {
            entitiy.Property(p => p.Id)
                   .ValueGeneratedOnAdd()
                   .HasColumnType("int")
                   .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            entitiy.Property(p => p.CreateTime)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("(GETUTCDATE())")
                   .IsRequired();

            entitiy.Property(p => p.Log)
                   .HasColumnType("nvarchar(max)")
                   .IsRequired();

            entitiy.HasKey(p => p.Id);
        });

        builder.Entity<I18n>(entitiy =>
        {
            entitiy.Property(p => p.Name)
                   .HasColumnType("nvarchar(50)")
                   .IsUnicode(false);

            entitiy.Property(p => p.NormalizedName)
                   .HasColumnType("nvarchar(50)")
                   .IsRequired()
                   .IsUnicode(false);

            entitiy.Property(p => p.ConcurrencyStamp)
                   .IsConcurrencyToken()
                   .HasColumnType("nvarchar(max)")
                   .IsRequired();

            entitiy.HasKey(p => p.Name);

            entitiy.HasIndex(p => p.NormalizedName)
                   .IsUnique();
        });
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        CommonEntitiesBuild(builder);
    }
}

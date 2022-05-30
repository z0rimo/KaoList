using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeRabbits.KaoList;

public static class ModelBuilderExentsion
{
    public static void KaoListEntitiesBuild(this ModelBuilder builder)
    {
        builder.Entity<AppLog>(b =>
        {
            b.HasKey(al => al.Id);

            b.Property(al => al.Id)
             .ValueGeneratedOnAdd()
             .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            b.Property(al => al.Created).IsRequired();
            b.Property(p => p.Log).IsRequired();
        });

        builder.Entity<Classfication>(b =>
        {
            b.HasKey(c => c.Id);
            b.HasIndex(c => c.DisplayName).HasDatabaseName("ClassficationNameIndex");
            b.ToTable("Classfications");
            b.Property(c => c.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(c => c.DisplayName).HasColumnType("nvarchar(256)");

            b.HasMany<ClassficationLocalized>().WithOne().HasForeignKey(cl => cl.ClassficationId).IsRequired();
        });

        builder.Entity<ClassficationLocalized>(b =>
        {
            b.HasKey(cl => new { cl.I18nName, cl.ClassficationId });
            b.HasIndex(cl => cl.DisplayName).HasDatabaseName("ClassficationLocalizedNameIndex"); ;
            b.ToTable("ClassficationLocalizeds");
            b.Property(cl => cl.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(cl => cl.DisplayName).HasColumnType("nvarchar(256)");
        });

        builder.Entity<I18n>(b =>
        {
            b.HasKey(i => i.Name);
            b.HasIndex(i => i.NormalizedName).IsUnique().HasDatabaseName("I18nNormalizedNameIndex");
            b.ToTable("I18ns");
            b.Property(i => i.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(i => i.Name).HasColumnType("nvarchar(50)");
            b.Property(i => i.NormalizedName).HasColumnType("nvarchar(50)").IsRequired();

            b.HasMany<ClassficationLocalized>().WithOne().HasForeignKey(cl => cl.I18nName).IsRequired();
        });
    }
}

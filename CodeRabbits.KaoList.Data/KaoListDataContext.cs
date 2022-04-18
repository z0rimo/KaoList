using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CodeRabbits.KaoList.Data;

public class KaoListDataContext : IdentityDbContext<KaoListUser>
{
    public KaoListDataContext(DbContextOptions options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; } = default!;
    public virtual DbSet<Tj> Tj { get; set; } = default!;
    public virtual DbSet<Ky> Ky { get; set; } = default!;
    public virtual DbSet<Bookmark> Bookmarks { get; set; } = default!;
    public virtual DbSet<Song> Songs { get; set; } = default!;
    public virtual DbSet<SongGenre> SongGenres { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<KaoListUser>(b =>
        {
            b.Property(p => p.NickName)
             .IsRequired();

            b.ToTable("KaoListUser");
        });

        builder.Entity<Song>(b =>
        {
            b.Property(p => p.Id)
             .ValueGeneratedOnAdd()
             .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            b.Property(p => p.Title)
             .IsRequired();

            b.Property(p => p.Artist)
             .IsRequired();

            b.Property(p => p.YouTubeId);

            b.HasKey(p => p.Id);
        });

        builder.Entity<Bookmark>(b =>
        {
            b.Property(p => p.UserId)
             .IsRequired();

            b.Property(p => p.SongId)
             .IsRequired();

            b.HasKey(p => new { p.UserId, p.SongId });
        });

        builder.Entity<Tj>(b =>
        {
            b.Property(p => p.No)
            .IsRequired();

            b.Property(p => p.SongId)
            .IsRequired();

            b.Property(p => p.CreateDate)
             .HasDefaultValueSql("(GETUTCDATE())")
             .IsRequired();

            b.HasKey(p => p.No);
        });

        builder.Entity<Ky>(b =>
        {
            b.Property(p => p.No)
            .IsRequired();

            b.Property(p => p.SongId)
            .IsRequired();

            b.Property(p => p.CreateDate)
            .IsRequired();

            b.HasKey(p => p.No);
        });

        builder.Entity<Genre>(b =>
        {
            b.Property(p => p.Id)
            .IsRequired();

            b.Property(p => p.Descriptopn)
            .IsRequired();

            b.HasKey(p => p.Id);
        });

        builder.Entity<SongGenre>(b =>
        {
            b.Property(p => p.SongId)
             .IsRequired();

            b.Property(p => p.SongGenreId)
             .IsRequired();

            b.HasKey(p => new { p.SongId, p.SongGenreId });
        });

        builder.Entity<Bookmark>(b =>
        {
            b.HasOne(s => s.Song)
             .WithMany(f => f.Bookmarks)
             .HasForeignKey(d => d.SongId)
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired();

            b.HasOne(s => s.User)
             .WithMany(f => f.Bookmarks)
             .HasForeignKey(d => d.UserId)
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired();
        });

        builder.Entity<Tj>()
               .HasOne(b => b.Songs)
               .WithOne(i => i.Tj)
               .HasForeignKey<Tj>(b => b.SongId)
               .HasPrincipalKey<Song>(p => p.Id);

        builder.Entity<Ky>()
               .HasOne(b => b.Songs)
               .WithOne(i => i.Ky)
               .HasForeignKey<Ky>(b => b.SongId)
               .HasPrincipalKey<Song>(p => p.Id);


        builder.Entity<SongGenre>(b =>
        {
            b.HasOne(s => s.Song)
             .WithMany(f => f.SongGenres)
             .HasForeignKey(d => d.SongId)
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired();

            b.HasOne(s => s.Genre)
             .WithMany(s => s.SongGenres)
             .HasForeignKey(d => d.SongGenreId)
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired();
        });
    }
}

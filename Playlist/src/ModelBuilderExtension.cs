// (c) 2022 CodeRabbits.
// This code is licensed under MIT license (see LICENSE.txt for details).

using CodeRabbits.KaoList.Song;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeRabbits.KaoList.Playlist;

public static class ModelBuilderExtension
{
    public static void KaoListPlaylistEntityBuilder<TUser>(this ModelBuilder builder) where TUser : class
    {
        builder.Entity<I18n>(b =>
        {
            b.HasMany<KaoListPlaylistLocalized>().WithOne().HasForeignKey(p => p.I18nName).IsRequired();
        });

        builder.Entity<Sing>(b =>
        {
            b.HasMany<KaoListPlaylistSingItem>().WithOne().HasForeignKey(p => p.SingId).IsRequired();
        });

        builder.Entity<KaoListPlaylist>(b =>
        {
            b.HasKey(p => p.Id);
            b.ToTable("KaoListPlaylist");
            b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(p => p.Name).HasColumnType("nvarchar(256)").IsRequired();
            b.Property(p => p.PrivacyStatus).HasColumnType("nvarchar(450)").IsRequired();

            b.HasMany<KaoListPlaylistLocalized>().WithOne().HasForeignKey(p => p.PlaylistId).IsRequired();
            b.HasMany<KaoListPlaylistSingItem>().WithOne().HasForeignKey(p => p.PlaylistId).IsRequired();
            b.HasMany<KaoListPlaylistShare>().WithOne().HasForeignKey(p => p.PlaylistId).IsRequired();
            b.HasMany<KaoListPlaylistPlayLog>().WithOne().HasForeignKey(p => p.PlaylistId).IsRequired();
            b.HasMany<YouTubePlaylistSyncInfo>().WithOne().HasForeignKey(p => p.PlaylistId).IsRequired();
        });

        builder.Entity<KaoListPlaylistSingItem>(b =>
        {
            b.HasKey(p => new { p.PlaylistId, p.SingId });
            b.ToTable("KaoListPlaylistSingItem");

            b.Property(p => p.CreateTime).IsRequired();
        });

        builder.Entity<KaoListPlaylistLocalized>(b =>
        {
            b.HasKey(p => new { p.PlaylistId, p.I18nName });
            b.ToTable("KaoListPlaylistLocalized");
            b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(p => p.Name).HasColumnType("nvarchar(256)").IsRequired();
        });

        builder.Entity<KaoListPlaylistPlayLog>(b =>
        {
            b.HasKey(p => p.Id);
            b.ToTable("KaoListPlaylistPlayLog");

            b.Property(p => p.Id)
             .ValueGeneratedOnAdd()
             .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            b.Property(p => p.IdentityToken).HasColumnType("nvarchar(450)").IsRequired();
            b.Property(p => p.CreateTime).IsRequired();
        });

        builder.Entity<KaoListPlaylistPrivacyState>(b =>
        {
            b.HasKey(p => p.Id);
            b.HasIndex(p => p.NormalizedName).IsUnique();
            b.ToTable("KaoListPlaylistPrivacyState");
            b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(p => p.Name).HasColumnType("nvarchar(256)");
            b.Property(p => p.NormalizedName).HasColumnType("nvarchar(256)");

            b.HasMany<KaoListPlaylist>().WithOne().HasForeignKey(p => p.PrivacyStatus);
        });

        builder.Entity<KaoListPlaylistShare>(b =>
        {
            b.HasKey(p => new { p.PlaylistId, p.UserId });
            b.ToTable("KaoListPlaylistShare");
        });

        builder.Entity<KaoListPlaylistShareRole>(b =>
        {
            b.HasKey(p => p.Id);
            b.HasIndex(p => p.NormalizedName).IsUnique();
            b.ToTable("KaoListPlaylistShareRole");
            b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(p => p.Name).HasColumnType("nvarchar(256)");
            b.Property(p => p.NormalizedName).HasColumnType("nvarchar(256)");

            b.HasMany<KaoListPlaylistShare>().WithOne().HasForeignKey(p => p.ShareRole).IsRequired();
        });

        builder.Entity<YouTubePlaylistShared>(b =>
        {
            b.HasKey(p => new { p.YouTubePlaylistId, p.UserId });
            b.ToTable("YouTubePlaylistShared");

            b.HasMany<YouTubePlaylistSyncInfo>()
            .WithOne()
            .HasForeignKey(p => p.YouTubePlaylistId)
            .HasPrincipalKey(p => p.YouTubePlaylistId)
            .IsRequired();
        });

        builder.Entity<YouTubePlaylistSyncInfo>(b =>
        {
            b.HasKey(p => new { p.YouTubePlaylistId, p.PlaylistId });
            b.ToTable("YouTubePlaylistSync");
        });

        builder.Entity<TUser>(b =>
        {
            b.HasMany<KaoListPlaylist>().WithOne().HasForeignKey(p => p.UserId).IsRequired();
            // When UserId is deleted, the PlaylistId is deleted, and when the PlaylistId is deleted, the PlaylistShare is deleted.
            // Therefore, PlaylistShare is not immediately removed when UserId is deleted.
            b.HasMany<KaoListPlaylistShare>().WithOne().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            // When UserId is deleted, the Playlist is deleted, and when the PlaylistPlayLog is deleted, the Playlist is deleted.
            // Therefore, PlaylistPlayLog is not immediately removed when UserId is deleted.
            b.HasMany<KaoListPlaylistPlayLog>().WithOne().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
        });
    }
}

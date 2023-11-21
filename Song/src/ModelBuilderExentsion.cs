using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeRabbits.KaoList.Song;

public static class ModelBuilderExentsion
{
    private sealed class PlayStateConverter : ValueConverter<PlayState?, string?>
    {
        public PlayStateConverter() : base(p => PlayStateToString(p), s => StringToPlayState(s)) { }

        private static string? PlayStateToString(PlayState? p) => p?.ToString() ?? null;
        private static PlayState? StringToPlayState(string? s) => s is not null ? (PlayState)Enum.Parse(typeof(PlayState), s) : null;
    }

    public static void SongEntitiesBuild<TUser>(this ModelBuilder builder) where TUser : class
    {
        builder.Entity<Classfication>(b =>
        {
            b.HasMany<InstrumentalClassification>().WithOne().HasForeignKey(il => il.ClassficationId).IsRequired();
        });

        builder.Entity<I18n>(b =>
        {
            b.HasMany<InstrumentalLocalized>().WithOne().HasForeignKey(il => il.I18nName).IsRequired();
        });

        builder.Entity<Instrumental>(b =>
        {
            b.HasKey(l => l.Id);
            b.HasIndex(l => l.NormalizedTitle).HasDatabaseName("InstrumentalNormalizedTitleIndex");
            b.ToTable("Instrumentals");
            b.Property(l => l.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(i => i.Title)
             .HasColumnType("nvarchar(256)")
             .IsRequired()
             .UseCollation("Latin1_General_100_CS_AS_KS_WS_SC_UTF8");

            b.Property(i => i.NormalizedTitle)
             .HasColumnType("nvarchar(256)")
             .IsRequired()
             .UseCollation("Latin1_General_100_CI_AI_SC_UTF8");

            b.Property(i => i.Composer).HasColumnType("nvarchar(256)");

            b.HasMany<InstrumentalClassification>().WithOne().HasForeignKey(ic => ic.InstrumentalId).IsRequired();
            b.HasMany<InstrumentalFollower>().WithOne().HasForeignKey(f => f.InstrumentalId).IsRequired();
            b.HasMany<InstrumentalLocalized>().WithOne().HasForeignKey(il => il.InstrumentalId).IsRequired();
            b.HasMany<Lyric>().WithOne().HasForeignKey(l => l.InstrumentalId).IsRequired();
            b.HasMany<Sing>().WithOne().HasForeignKey(s => s.InstrumentalId).IsRequired();
            // When Instrumental is deleted, the Sing is deleted, and when the Sing is deleted, the TitleSing is deleted.
            // Therefore, TitleSing is not immediately removed when Instrumental is deleted.
            b.HasMany<TitleSing>().WithOne().HasForeignKey(ts => ts.InstrumentalId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<InstrumentalBlind>(b =>
        {
            b.HasKey(ib => new { ib.InstrumentalId, ib.UserId });
            b.ToTable("InstrumentalBlinds");

            b.Property(ib => ib.Created).IsRequired();
        });

        builder.Entity<InstrumentalClassification>(b =>
        {
            b.HasKey(ic => new { ic.ClassficationId, ic.InstrumentalId });
            b.ToTable("InstrumentalClassifications");
        });

        builder.Entity<InstrumentalFollower>(b =>
        {
            b.HasKey(f => new { f.InstrumentalId, f.UserId });
            b.ToTable("InstrumentalFollowers");

            b.Property(f => f.Created).IsRequired();
        });

        builder.Entity<InstrumentalLocalized>(b =>
        {
            b.HasKey(il => new { il.InstrumentalId, il.I18nName });
            b.HasIndex(il => il.NormalizedTitle).HasDatabaseName("InstrumentalLocalizedNormalizedTitleIndex"); ;
            b.ToTable("InstrumentalLocalizeds");
            b.Property(f => f.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(il => il.Title)
             .HasColumnType("nvarchar(256)")
             .IsRequired()
             .UseCollation("Latin1_General_100_CS_AS_KS_WS_SC_UTF8");

            b.Property(il => il.NormalizedTitle)
             .HasColumnType("nvarchar(256)")
             .IsRequired()
             .UseCollation("Latin1_General_100_CI_AI_SC_UTF8");

        });

        builder.Entity<Karaoke>(b =>
        {
            b.HasKey(k => new { k.Provider, k.No });
            b.ToTable("Karaokes");
            b.Property(f => f.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(k => k.Provider).HasColumnType("nvarchar(256)");
            b.Property(k => k.No).HasColumnType("nvarchar(50)");
        });

        builder.Entity<Lyric>(b =>
        {
            b.HasKey(l => new { l.Sequence, l.InstrumentalId });
            b.ToTable("Lyrics");

            b.Property(l => l.Offset).IsRequired();
            b.Property(l => l.Duration).IsRequired();

            b.Property(il => il.Content)
             .UseCollation("Latin1_General_100_CS_AS_KS_WS_SC_UTF8");

            b.Property(il => il.NormalizedContent)
             .UseCollation("Latin1_General_100_CI_AI_SC_UTF8");
        });

        builder.Entity<PopularDailySing>(b =>
        {
            b.HasKey(pds => new { pds.Created, pds.SingId });
            b.ToTable("PopularDailySings");
            b.Property(pds => pds.Score).IsRequired();
        });

        builder.Entity<PopularSing>(b =>
        {
            b.HasKey(ps => new { ps.Created, ps.SingId });
            b.ToTable("PopularSings");

            b.Property(ps => ps.Score).IsRequired();
        });

        builder.Entity<Sing>(b =>
        {
            b.HasKey(s => s.Id);
            b.ToTable("Sings");

            b.Property(s => s.Created).IsRequired();

            b.HasMany<Karaoke>().WithOne().HasForeignKey(k => k.SingId).IsRequired();
            b.HasMany<PopularDailySing>().WithOne().HasForeignKey(ib => ib.SingId).IsRequired();
            b.HasMany<PopularSing>().WithOne().HasForeignKey(ib => ib.SingId).IsRequired();
            b.HasMany<SingBlind>().WithOne().HasForeignKey(ib => ib.SingId).IsRequired();
            b.HasMany<SingFollower>().WithOne().HasForeignKey(ib => ib.SingId).IsRequired();
            b.HasMany<SingUser>().WithOne().HasForeignKey(ib => ib.SingId).IsRequired();
            b.HasMany<SongDetailLog>().WithOne().HasForeignKey(sdl => sdl.SingId).IsRequired();
            b.HasMany<SongSearchLog>().WithOne().HasForeignKey(ssl => ssl.SingId).IsRequired();
            b.HasMany<TitleSing>().WithOne().HasForeignKey(ib => ib.SingId).IsRequired();
        });

        builder.Entity<SingBlind>(b =>
        {
            b.HasKey(sb => new { sb.SingId, sb.UserId });
            b.ToTable("SingBlinds");

            b.Property(s => s.Created).IsRequired();
        });

        builder.Entity<SingFollower>(b =>
        {
            b.HasKey(sf => new { sf.SingId, sf.UserId });
            b.ToTable("SingFollowers");

            b.Property(sf => sf.Created).IsRequired().HasDefaultValueSql("GETUTCDATE()");
        });

        builder.Entity<SingUser>(b =>
        {
            b.HasKey(su => new { su.SingId, su.UserId });
            b.ToTable("SingUsers");
        });

        builder.Entity<SongDetailLog>(b =>
        {
            b.HasKey(sdl => sdl.Id);
            b.ToTable("SongDetailLogs");
            b.Property(sdl => sdl.Created).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            b.Property(sdl => sdl.IdentityToken).HasColumnType("nvarchar(450)").IsRequired();
        });

        builder.Entity<SongSearchLog>(b =>
        {
            b.HasKey(sl => sl.Id);
            b.ToTable("SongSearchLogs");

            b.Property(sl => sl.Query).IsRequired();
            b.Property(sl => sl.Created).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            b.Property(sl => sl.IdentityToken).HasColumnType("nvarchar(450)").IsRequired();
        });

        builder.Entity<Sound>(b =>
        {
            b.HasKey(s => s.Id);
            b.ToTable("Sounds");

            b.Property(s => s.Path).IsRequired();

            b.HasMany<Instrumental>().WithOne().HasForeignKey(ib => ib.SoundId);
            b.HasMany<Sing>().WithOne().HasForeignKey(ib => ib.SoundId);
            b.HasMany<SoundPlayLog>().WithOne().HasForeignKey(ib => ib.SoundId).IsRequired();
        });

        builder.Entity<SoundPlayLog>(b =>
        {
            b.HasKey(pl => pl.Id);
            b.ToTable("SoundPlayLogs");

            b.Property(pl => pl.IdentityToken).HasColumnType("nvarchar(450)").IsRequired();

            b.HasMany<SoundPlaylogDetail>().WithOne().HasForeignKey(ib => ib.SoundPlayLogId).IsRequired();
        });

        builder.Entity<SoundPlaylogDetail>(b =>
        {
            b.HasKey(pd => pd.Id);
            b.ToTable("SoundPlaylogDetails");

            b.Property(pl => pl.CurrentTime).IsRequired();
            b.Property(pl => pl.Status).HasConversion<PlayStateConverter>().IsRequired();
            b.Property(sf => sf.Created).IsRequired();
        });

        builder.Entity<TitleSing>(b =>
        {
            b.HasKey(ts => new { ts.UserId, ts.InstrumentalId });
            b.ToTable("TitleSings");
        });

        builder.Entity<TUser>(b =>
        {
            b.HasMany<InstrumentalBlind>().WithOne().HasForeignKey(ib => ib.UserId).IsRequired();
            b.HasMany<InstrumentalFollower>().WithOne().HasForeignKey(f => f.UserId).IsRequired();
            b.HasMany<SingBlind>().WithOne().HasForeignKey(sa => sa.UserId).IsRequired();
            b.HasMany<SingFollower>().WithOne().HasForeignKey(sf => sf.UserId).IsRequired();
            b.HasMany<SingUser>().WithOne().HasForeignKey(su => su.UserId).IsRequired();
            b.HasMany<SongDetailLog>().WithOne().HasForeignKey(sdl => sdl.UserId).OnDelete(DeleteBehavior.SetNull);
            b.HasMany<SongSearchLog>().WithOne().HasForeignKey(sp => sp.UserId).OnDelete(DeleteBehavior.SetNull);
            b.HasMany<SoundPlayLog>().WithOne().HasForeignKey(sp => sp.UserId).OnDelete(DeleteBehavior.SetNull);
            b.HasMany<TitleSing>().WithOne().HasForeignKey(ts => ts.UserId).IsRequired();
        });
    }

}

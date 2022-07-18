using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeRabbits.KaoList.Board;

public static class ModelBuilderExtension
{
    public static void BoardEntitiesBuild<TUser>(this ModelBuilder builder) where TUser : class
    {
        builder.Entity<I18n>(b =>
        {
            b.HasMany<HeadLocalized>().WithOne().HasForeignKey(hl => hl.I18nName).IsRequired();
        });

        builder.Entity<TUser>(b =>
        {
            //Even if userid is deleted, the user who reported the comment can be distinguished with the identity token.
            //And the content that reported comment should remain, so set it to set null.
            b.HasMany<CommentReport>().WithOne().HasForeignKey(cr => cr.UserId).OnDelete(DeleteBehavior.SetNull);
            b.HasMany<PostCommentUser>().WithOne().HasForeignKey(cu => cu.UserId).IsRequired();
            //Even if the UserId is deleted, the record is not deleted because the user is identified by the token and the UserId is set to null.
            b.HasMany<PostHitLog>().WithOne().HasForeignKey(hl => hl.UserId).OnDelete(DeleteBehavior.SetNull);
            //Even if the UserId is deleted, the record is not deleted because the user is identified by the token and the UserId is set to null.
            b.HasMany<PostLike>().WithOne().HasForeignKey(pl => pl.UserId).OnDelete(DeleteBehavior.SetNull);
            //Even if the UserId is deleted, the record is not deleted because the user is identified by the token and the UserId is set to null.
            b.HasMany<PostReport>().WithOne().HasForeignKey(pr => pr.UserId).OnDelete(DeleteBehavior.SetNull);
            //Even if the UserId is deleted, the record is not deleted because the user is identified by the token and the UserId is set to null.
            b.HasMany<PostUnlike>().WithOne().HasForeignKey(pu => pu.UserId).OnDelete(DeleteBehavior.SetNull);
            b.HasMany<PostUser>().WithOne().HasForeignKey(pu => pu.UserId).IsRequired();
        });

        builder.Entity<CommentReport>(b =>
        {
            b.HasKey(cr => cr.Id);
            b.ToTable("CommentReports");

            b.Property(p => p.CreateTime).IsRequired();
            b.Property(p => p.UserId).HasColumnType("nvarchar(450)");
            b.Property(p => p.IdentityToken).HasColumnType("nvarchar(450)");
        });

        builder.Entity<Head>(b =>
        {
            b.HasIndex(h => h.DisplayName).HasDatabaseName("HeadDisplayNameIndex");
            b.HasIndex(h => h.NomalizedDisplayName).IsUnique();
            b.HasKey(h => h.Id);
            b.ToTable("Heads");
            b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(p => p.DisplayName).HasColumnType("nvarchar(256)").IsRequired();
            b.Property(p => p.NomalizedDisplayName).HasColumnType("nvarchar(256)").IsRequired();

            b.HasMany<HeadLocalized>().WithOne().HasForeignKey(hl => hl.HeadId).IsRequired();
            b.HasMany<PostHead>().WithOne().HasForeignKey(ph => ph.HeadId).IsRequired();
        });

        builder.Entity<HeadLocalized>(b =>
        {
            b.HasIndex(hl => hl.Displayname).HasDatabaseName("HeadLocalizedDisplayNameIndex");
            b.HasKey(hl => new { hl.HeadId, hl.I18nName });
            b.ToTable("HeadLocalizeds");
            b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(p => p.I18nName).HasColumnType("nvarchar(50)").IsRequired();
            b.Property(p => p.Displayname).HasColumnType("nvarchar(256)").IsRequired();
        });

        builder.Entity<OriginalPost>(b =>
        {
            b.HasKey(op => op.Id);
            b.ToTable("OriginalPosts");

            b.Property(p => p.PostId).IsRequired();
            b.Property(p => p.Title).HasColumnType("nvarchar(256)");
            b.Property(p => p.CreateTime).IsRequired();
        });

        builder.Entity<OriginalPostComment>(b =>
        {
            b.HasKey(opc => opc.Id);
            b.ToTable("OriginalPostComments");

            b.Property(p => p.PostCommentId).IsRequired();
            b.Property(p => p.CreateTime).IsRequired();
        });

        builder.Entity<Post>(b =>
        {
            b.HasKey(po => po.Id);
            b.ToTable("Posts");
            b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            b.Property(p => p.CreateTime).IsRequired();
            b.Property(p => p.Title).HasColumnType("nvarchar(256)");
            b.Property(p => p.RemoveRequestTime);
            b.Property(p => p.Blinded).IsRequired();

            b.HasMany<OriginalPost>().WithOne().HasForeignKey(cr => cr.PostId).IsRequired();
            b.HasMany<PostChart>().WithOne().HasForeignKey(pc => pc.PostId).IsRequired();
            b.HasMany<PostComment>().WithOne().HasForeignKey(pc => pc.PostId).IsRequired();
            b.HasMany<PostHead>().WithOne().HasForeignKey(ph => ph.PostId).IsRequired();
            b.HasMany<PostHitLog>().WithOne().HasForeignKey(phl => phl.PostId).IsRequired();
            b.HasMany<PostLike>().WithOne().HasForeignKey(pl => pl.PostId).IsRequired();
            b.HasMany<PostReport>().WithOne().HasForeignKey(pr => pr.PostId).IsRequired();
            b.HasMany<PostUnlike>().WithOne().HasForeignKey(pu => pu.PostId).IsRequired();
            b.HasMany<PostUser>().WithOne().HasForeignKey(pu => pu.PostId).IsRequired();
        });

        builder.Entity<PostChart>(b =>
        {
            b.HasIndex(pc => pc.Title).HasDatabaseName("PostChartTitleIndex");
            b.HasKey(pc => pc.Id);
            b.ToTable("PostCharts");

            b.Property(p => p.PostId).IsRequired();
            b.Property(p => p.Title).HasColumnType("nvarchar(256)");
            b.Property(p => p.VoteNumber).IsRequired();
            b.Property(p => p.EndDateTime).IsRequired();
            b.Property(p => p.VoteRole).HasColumnType("nvarchar(450)").IsRequired();

            b.HasMany<PostChartItem>().WithOne().HasForeignKey(pci => pci.PostChartId).IsRequired();
            b.HasMany<PostChartVote>().WithOne().HasForeignKey(pcv => pcv.PostChartItemId).IsRequired();
        });

        builder.Entity<PostChartItem>(b =>
        {
            b.HasIndex(pci => pci.Title).HasDatabaseName("PostChartItemTitleIndex");
            b.HasKey(pci => pci.Id);
            b.ToTable("PostChartItems");

            b.Property(p => p.PostChartId).HasColumnType("nvarchar(450)").IsRequired();
            b.Property(p => p.Title).HasColumnType("nvarchar(256)");
        });

        builder.Entity<PostChartVote>(b =>
        {
            b.HasKey(pcv => pcv.Id);
            b.ToTable("PostChartVotes");

            b.Property(p => p.PostChartItemId).HasColumnType("nvarchar(450)").IsRequired();
            b.Property(p => p.CreateTime).IsRequired();
            b.Property(p => p.IdentityToken).HasColumnType("nvarchar(450)").IsRequired();
        });

        builder.Entity<PostChartVoteRole>(b =>
        {
            b.HasIndex(pcvr => pcvr.NormalizedName).IsUnique();
            b.HasKey(pcvr => pcvr.Id);
            b.ToTable("PostChartVoteRoles");
            b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(p => p.Name).HasColumnType("nvarchar(256)");
            b.Property(p => p.NormalizedName).HasColumnType("nvarchar(256)");
            b.HasMany<PostChart>().WithOne().HasForeignKey(pc => pc.VoteRole).IsRequired();
        });

        builder.Entity<PostComment>(b =>
        {
            b.HasKey(pc => pc.Id);
            b.ToTable("PostComments");
            b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(p => p.Id)
             .ValueGeneratedOnAdd()
             .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            b.Property(p => p.PostId).IsRequired();
            b.Property(p => p.CreateTime).IsRequired();
            b.Property(p => p.Blinded).IsRequired();

            b.HasMany<CommentReport>().WithOne().HasForeignKey(cr => cr.CommentId).IsRequired();
            b.HasMany<OriginalPostComment>().WithOne().HasForeignKey(opc => opc.PostCommentId).IsRequired();
            //PostComment is set to NoAction as it refers to PostParent by itself.
            b.HasMany<PostComment>().WithOne().HasForeignKey(pc => pc.CommentParent).OnDelete(DeleteBehavior.NoAction);
            b.HasMany<PostCommentUser>().WithOne().HasForeignKey(cu => cu.PostCommantId).IsRequired();
        });

        builder.Entity<PostCommentUser>(b =>
        {
            b.HasKey(pc => pc.PostCommantId);
            b.ToTable("PostCommentUsers");
        });

        builder.Entity<PostHead>(b =>
        {
            b.HasKey(ph => ph.HeadId);
            b.ToTable("PostHeads");

            b.Property(p => p.PostId).IsRequired();
        });

        builder.Entity<PostHitLog>(b =>
        {
            b.HasKey(phl => phl.Id);
            b.ToTable("PostHitLogs");

            b.Property(p => p.PostId).IsRequired();
            b.Property(p => p.CreateTime).IsRequired();
            b.Property(p => p.UserId).HasColumnType("nvarchar(450)");
            b.Property(p => p.IdentityToken).HasColumnType("nvarchar(450)").IsRequired();
        });

        builder.Entity<PostLike>(b =>
        {
            b.HasKey(pl => pl.Id);
            b.ToTable("PostLikes");

            b.Property(p => p.PostId).IsRequired();
            b.Property(p => p.CreateTime).IsRequired();
            b.Property(p => p.UserId).HasColumnType("nvarchar(450)");
            b.Property(p => p.IdentityToken).HasColumnType("nvarchar(450)");
        });

        builder.Entity<PostReport>(b =>
        {
            b.HasKey(pr => pr.Id);
            b.ToTable("PostReports");

            b.Property(p => p.PostId).IsRequired();
            b.Property(p => p.CreateTime).IsRequired();
            b.Property(p => p.UserId).HasColumnType("nvarchar(450)");
            b.Property(p => p.IdentityToken).HasColumnType("nvarchar(450)");
        });

        builder.Entity<PostUnlike>(b =>
        {
            b.HasKey(pu => pu.Id);
            b.ToTable("PostUnlikes");

            b.Property(p => p.PostId).IsRequired();
            b.Property(p => p.CreateTime).IsRequired();
            b.Property(p => p.UserId).HasColumnType("nvarchar(450)");
            b.Property(p => p.IdentityToken).HasColumnType("nvarchar(450)");
        });

        builder.Entity<PostUser>(b =>
        {
            b.HasKey(pu => pu.PostId);
            b.ToTable("PostUsers");
        });
    }
}
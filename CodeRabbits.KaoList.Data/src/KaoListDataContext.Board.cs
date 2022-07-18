using CodeRabbits.KaoList.Board;
using Microsoft.EntityFrameworkCore;

namespace CodeRabbits.KaoList.Data;

public partial class KaoListDataContext
{
    public virtual DbSet<CommentReport> CommentReports { get; set; } = default!;
    public virtual DbSet<Head> Heads { get; set; } = default!;
    public virtual DbSet<HeadLocalized> HeadLocalizeds { get; set; } = default!;
    public virtual DbSet<OriginalPost> OriginalPost { get; set; } = default!;
    public virtual DbSet<OriginalPostComment> OriginalPsotComments { get; set; } = default!;
    public virtual DbSet<Post> Posts { get; set; } = default!;
    public virtual DbSet<PostChart> PostCharts { get; set; } = default!;
    public virtual DbSet<PostChartItem> PostChartItems { get; set; } = default!;
    public virtual DbSet<PostChartVote> PostChartVotes { get; set; } = default!;
    public virtual DbSet<PostChartVoteRole> PostChartVoteRoles { get; set; } = default!;
    public virtual DbSet<PostComment> PostComments { get; set; } = default!;
    public virtual DbSet<PostCommentUser> PostCommentUsers { get; set; } = default!;
    public virtual DbSet<PostHead> PostHeads { get; set; } = default!;
    public virtual DbSet<PostHitLog> PostHitLogs { get; set; } = default!;
    public virtual DbSet<PostLike> PostLikes { get; set; } = default!;
    public virtual DbSet<PostReport> PostReports { get; set; } = default!;
    public virtual DbSet<PostUnlike> PostUnlikes { get; set; } = default!;
    public virtual DbSet<PostUser> PostUsers { get; set; } = default!;
}
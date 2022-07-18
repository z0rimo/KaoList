using CodeRabbits.KaoList.Board;
using CodeRabbits.KaoList.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace CodeRabbits.KaoList.Data.Test
{
    public class KaoListDataContextValidationTest
    {
        PropertyInfo? GetPropertyOrNull(Type c, Type property)
        {
            return c.GetProperties().SingleOrDefault(p => p.PropertyType == property);
        }

        [Fact]
        public void DbSetValidationTest()
        {
            var kaoListDataContextType = typeof(KaoListDataContext);
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<AppLog>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<CommentReport>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<Head>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<HeadLocalized>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<I18n>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<KaoListUser>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<KaoListUserChannel>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<KaoListUserColor>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<KaoListUserDeleteReason>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<KaoListUserFollower>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<KaoListUserLocalized>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<OriginalPost>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<OriginalPostComment>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<Post>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<PostChart>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<PostChartItem>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<PostChartVote>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<PostChartVoteRole>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<PostComment>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<PostCommentUser>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<PostHead>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<PostHitLog>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<PostLike>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<PostReport>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<PostUnlike>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<PostUser>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<SignInAttempt>)));
        }
    }
}
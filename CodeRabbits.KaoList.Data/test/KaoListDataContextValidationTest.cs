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
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<I18n>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<KaoListUser>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<KaoListUserChannel>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<KaoListUserColor>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<KaoListUserDeleteReason>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<KaoListUserFollower>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<KaoListUserLocalized>)));
            Assert.NotNull(GetPropertyOrNull(kaoListDataContextType, typeof(DbSet<SignInAttempt>)));
        }

    }
}
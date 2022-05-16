using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Xunit;

namespace CodeRabbits.KaoList.Data.Test
{
    public class KaoListDataContextValidationTest
    {
        void ThrowIfUndefinedPropertyOfType(Type c, Type property)
        {
            _ = c.GetProperties().Single(p => p.PropertyType == property);
        }

        [Fact]
        public void DbSetValidationTest()
        {
            var kaoListDataContextType = typeof(KaoListDataContext);
            ThrowIfUndefinedPropertyOfType(kaoListDataContextType, typeof(DbSet<AppLog>));
            ThrowIfUndefinedPropertyOfType(kaoListDataContextType, typeof(DbSet<I18n>));
        }

    }
}
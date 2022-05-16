using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace CodeRabbits.KaoList.Data.Test
{
    public class I18nValidationTest
    {
        public static readonly object?[][] CorrectData =
        {
            new object?[] { null, null, null },
            new object?[] { "en-US", "EN-US", "dfbda069-a85c-413f-a870-3dbb666367a6"},
        };

        [Theory, MemberData(nameof(CorrectData))]
        public void TypeValidationTest(string? name, string? normalizedName, string concurrencyStamp)
        {
            var i18n = new I18n
            {
                ConcurrencyStamp = concurrencyStamp,
                Name = name,
                NormalizedName = normalizedName
            };

            Assert.Equal(i18n.ConcurrencyStamp, concurrencyStamp);
            Assert.Equal(i18n.Name, name);
            Assert.Equal(i18n.NormalizedName, normalizedName);
        }

        [Fact]
        public void AttributeValidationTest()
        {
            var I18nType = typeof(I18n);

            var indexAttribute = (IndexAttribute)I18nType.GetCustomAttributes(typeof(IndexAttribute), false).Single();
            Assert.Equal<string>(new string[] { nameof(I18n.NormalizedName) }, indexAttribute.PropertyNames);
            Assert.True(indexAttribute.IsUnique);

            Assert.Single(I18nType.GetProperty(nameof(I18n.ConcurrencyStamp))!.GetCustomAttributes(typeof(RequiredAttribute), false));
            Assert.Single(I18nType.GetProperty(nameof(I18n.ConcurrencyStamp))!.GetCustomAttributes(typeof(ConcurrencyCheckAttribute), false));

            Assert.Single(I18nType.GetProperty(nameof(I18n.Name))!.GetCustomAttributes(typeof(KeyAttribute), false));
            MaxLengthAttribute nameMaxLengthAttribute = (MaxLengthAttribute)I18nType.GetProperty(nameof(I18n.Name))
                !.GetCustomAttributes(typeof(MaxLengthAttribute), false)
                .Single();
            Assert.Equal(50, nameMaxLengthAttribute.Length);

            Assert.Single(I18nType.GetProperty(nameof(I18n.NormalizedName))!.GetCustomAttributes(typeof(RequiredAttribute), false));
            var normalizedNameMaxLengthAttribute = (MaxLengthAttribute)I18nType.GetProperty(nameof(I18n.NormalizedName))
                !.GetCustomAttributes(typeof(MaxLengthAttribute), false)
                .Single();
            Assert.Equal(50, normalizedNameMaxLengthAttribute.Length);

        }
    }
}
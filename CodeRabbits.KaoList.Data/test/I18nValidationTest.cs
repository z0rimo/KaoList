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
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CodeRabbits.KaoList.Data.Test
{
    public class AppLogValidationTest
    {
        public static readonly object?[][] CorrectData =
        {
            new object?[] { null, null, null },
            new object?[] { int.MinValue, DateTime.MaxValue, "  \t \t     \t  " },
        };

        [Theory, MemberData(nameof(CorrectData))]
        public void TypeValidationTest(int? id, DateTime? creatTime, string log)
        {
            var appLog = new AppLog
            {
                Id = id,
                CreateTime = creatTime,
                Log = log
            };

            Assert.Equal(appLog.Id, id);
            Assert.Equal(appLog.CreateTime, creatTime);
            Assert.Equal(appLog.Log, log);
        }

        [Fact]
        public void AttributeValidationTest()
        {
            var appLogType = typeof(AppLog);
            Assert.Single(appLogType.GetProperty(nameof(AppLog.Id))!.GetCustomAttributes(typeof(KeyAttribute), false));
            Assert.Single(appLogType.GetProperty(nameof(AppLog.CreateTime))!.GetCustomAttributes(typeof(RequiredAttribute), false));
            Assert.Single(appLogType.GetProperty(nameof(AppLog.Log))!.GetCustomAttributes(typeof(RequiredAttribute), false));
        }
    }
}
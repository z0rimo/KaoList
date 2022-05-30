using System;
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
                Created = creatTime,
                Log = log
            };

            Assert.Equal(appLog.Id, id);
            Assert.Equal(appLog.Created, creatTime);
            Assert.Equal(appLog.Log, log);
        }
    }
}
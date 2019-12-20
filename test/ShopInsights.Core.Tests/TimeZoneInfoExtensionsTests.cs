using System;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ShopInsights.Core.Tests
{
    public class TimeZoneInfoExtensionsTests
    {
        readonly ITestOutputHelper _outputHelper;

        public TimeZoneInfoExtensionsTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        [Fact]
        public void TestName()
        {
            var timeZone = GetDefaultTimeZone();

            var dateTimeOffset = new DateTimeOffset(2019,1,1,0,0,0, TimeSpan.FromHours(2));
            var result = timeZone.GetTimeZoneCorrectedDate(dateTimeOffset);

            result.Month.Should().Be(12);
            result.Year.Should().Be(2018);
            result.Day.Should().Be(31);
        }

        private static TimeZoneInfo GetDefaultTimeZone()
        {
            return TimeZoneInfo
                       .GetSystemTimeZones()
                       .FirstOrDefault(ti => ti.StandardName == "W. Europe Standard Time")
                   ??
                   TimeZoneInfo.GetSystemTimeZones()
                       .First(ti => ti.Id == "Europe/Berlin");
        }
    }
}

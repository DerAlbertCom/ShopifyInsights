using System;

namespace ShopInsights.Core
{
    public static class TimeZoneInfoExtensions {

        public static DateTime GetTimeZoneCorrectedDate(this TimeZoneInfo timeZone, DateTime dateTime)
        {
            var offset = timeZone.GetUtcOffset(dateTime);
            var correctedDate = dateTime.ToUniversalTime().Add(offset);
            return correctedDate.Date;
        }

        public static DateTime GetTimeZoneCorrectedDate(this TimeZoneInfo timeZone, DateTimeOffset dateTimeOffset)
        {
            var offset = timeZone.GetUtcOffset(dateTimeOffset);
            var correctedDate = dateTimeOffset.ToUniversalTime().Add(offset);
            return correctedDate.Date;
        }
    }
}

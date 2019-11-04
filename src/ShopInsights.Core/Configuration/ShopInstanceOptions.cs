using System;

namespace ShopInsights.Configuration
{
    public class ShopInstanceOptions
    {
        public TimeZoneInfo TimeZoneInfo { get; private set; } = TimeZoneInfo.Local;

        public string TimeZoneId
        {
            get => TimeZoneInfo.Id;
            set
            {
                try
                {
                    TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(value);
                }
                catch (TimeZoneNotFoundException)
                {
                }
            }
        }
    }
}

using System;

namespace TrainTimeliness.Client
{
    public static class DateTimeExtensions
    {
        public static string ToHspDate(this DateTime that)
        {
            return that.ToString("yyyy-MM-dd");
        }
    }
}

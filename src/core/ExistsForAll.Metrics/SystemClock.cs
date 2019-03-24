using System;

namespace ExistsForAll.Metrics
{
    public class SystemClock : ISystemClock
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
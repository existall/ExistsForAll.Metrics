using System;

namespace ExistsForAll.Metrics.Tests
{
    internal class TestClock : ISystemClock
    {
        public DateTimeOffset TestTime { get; set; }

        public DateTimeOffset Now()
        {
            return TestTime;
        }
    }
}
using System;

namespace ExistsForAll.Metrics
{
    public interface ISystemClock
    {
        DateTimeOffset Now();
    }
}
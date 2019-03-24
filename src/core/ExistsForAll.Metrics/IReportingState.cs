using System;

namespace ExistsForAll.Metrics
{
    internal interface IReportingState
    {
        bool IsEnabled();
        void Enable();
        void Suspend(TimeSpan timeSpan);
        void Disable();
    }
}

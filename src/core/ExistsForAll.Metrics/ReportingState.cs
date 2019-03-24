using System;
using System.Threading;

namespace ExistsForAll.Metrics
{
    internal class ReportingState : IReportingState
    {
        private const long ZeroState = 0;
        private readonly MetricsOptions _metricsOptions;
        private long _suspentionLimit;

        public ReportingState(MetricsOptions metricsOptions)
        {
            _metricsOptions = metricsOptions;
        }

        public bool IsEnabled()
        {
            if (_suspentionLimit == ZeroState)
                return true;

            if (_metricsOptions.SystemClock.Now().Ticks >= _suspentionLimit )
            {
                Interlocked.Exchange(ref _suspentionLimit, ZeroState);
                return true;
            }

            return false;
        }

        public void Enable()
        {
            Interlocked.Exchange(ref _suspentionLimit, ZeroState);
        }

        public void Suspend(TimeSpan timeSpan)
        {
            Interlocked.Exchange(ref _suspentionLimit, _metricsOptions.SystemClock.Now().AddTicks(timeSpan.Ticks).Ticks);
        }

        public void Disable()
        {
            Interlocked.Exchange(ref _suspentionLimit, TimeSpan.MaxValue.Ticks);
        }
    }
}

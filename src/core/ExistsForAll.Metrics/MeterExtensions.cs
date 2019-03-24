using System;
using System.Diagnostics;

namespace ExistsForAll.Metrics
{
    public static class MeterExtensions
    {
        public const string DurationKey = "colony.duration";

        public static IDisposable TimeScope(this Meter meter, MetricInfo metricInfo, string timeElapsedKey = DurationKey)
        {
            var stopWatch = new Stopwatch();

            return new ExecutionScope(stopWatch.Start, () =>
            {
                stopWatch.Stop();
                metricInfo.Data.Add(timeElapsedKey, stopWatch.Elapsed);
                meter.Send(metricInfo);
            });
        }

        public static IDisposable ConditionalTimeScope(this Meter target, MetricInfo metricInfo, Func<bool> condition, string timeElapsedKey = DurationKey)
        {
            var stopWatch = new Stopwatch();

            return new ExecutionScope(stopWatch.Start, () =>
            {
                stopWatch.Stop();

                if (!condition())
                    return;

                metricInfo.Data.Add(timeElapsedKey, stopWatch.Elapsed);
                target.Send(metricInfo);
            });
        }

        public static void ConditionalTimeScope(this Meter target,
            MetricInfo metricInfo,
            Action action,
            Func<bool> condition,
            string timeElapsedKey = DurationKey)
        {
            using (target.ConditionalTimeScope(metricInfo, condition, timeElapsedKey))
            {
                action();
            }
        }

        public static void TimeScope(this Meter target, MetricInfo metricInfo, Action action, string timeElapsedKey = DurationKey)
        {
            using (target.TimeScope(metricInfo, timeElapsedKey))
            {
                action();
            }
        }
    }
}

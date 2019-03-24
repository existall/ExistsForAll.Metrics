using System;
using System.Diagnostics;

namespace ExistsForAll.Metrics
{
    public static class DataMeterExtensions
    {
        public const string DurationKey = MeterExtensions.DurationKey;

        public static DataMeter AddData(this DataMeter target, string key, object value)
        {
            target.Data.Add(key, value);
            return target;
        }

        public static IDisposable TimeScope(this DataMeter target, string timeElapsedKey = DurationKey)
        {
            var stopWatch = new Stopwatch();

            return new ExecutionScope(stopWatch.Start, () =>
            {
                stopWatch.Stop();
                target.Data.Add(timeElapsedKey, stopWatch.Elapsed.TotalMilliseconds);
                target.Send();
            });
        }

        public static IDisposable ConditionalTimeScope(this DataMeter target, Func<bool> condition,
            string timeElapsedKey = DurationKey)
        {
            var stopWatch = new Stopwatch();

            return new ExecutionScope(stopWatch.Start, () =>
            {
                stopWatch.Stop();

                if (!condition())
                    return;

                target.Data.Add(timeElapsedKey, stopWatch.Elapsed.TotalMilliseconds);
                target.Send();
            });
        }

        public static void ConditionalTimeScope(this DataMeter target,
            Action action,
            Func<bool> condition,
            string timeElapsedKey = DurationKey)
        {
            using (target.ConditionalTimeScope(condition, timeElapsedKey))
            {
                action();
            }
        }

        public static void TimeScope(this DataMeter target, Action action, string timeElapsedKey = DurationKey)
        {
            using (target.TimeScope(timeElapsedKey))
            {
                action();
            }
        }
    }
}

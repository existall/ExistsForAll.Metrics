using System;
using System.Collections.Generic;

namespace ExistsForAll.Metrics
{
    public static class MetricSpaceExtensions
    {
        public static DataMeter DataMeter(this MetricSpace metricSpace, string key)
        {
            return new DataMeter(metricSpace.Meter(key));
        }

        public static MetricSpace AddReporter(this MetricSpace target, IMetricReporter reporter)
        {
            target.Options.Reporters.AddReporter(reporter);
            return target;
        }

        public static MetricSpace AddReporters(this MetricSpace target, IEnumerable<IMetricReporter> reporters)
        {
            target.Options.Reporters.AddReporters(reporters);
            return target;
        }

        public static void Disable(this MetricSpace space)
        {
            space.Options.ReportingState.Disable();
        }

        public static void Enable(this MetricSpace space)
        {
            space.Options.ReportingState.Enable();
        }

        public static void Suspend(this MetricSpace space, TimeSpan untill)
        {
            space.Options.ReportingState.Suspend(untill);
        }

        public static bool IsEnabled(this MetricSpace space)
        {
            return space.Options.ReportingState.IsEnabled();
        }
    }
}

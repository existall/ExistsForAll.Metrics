using ExistsForAll.Metrics;

namespace ExistsForAll.Metrics.Tests
{
    public class ReportsRecorder : IMetricReporter
    {
        public MetricInfo Last { get; private set; }

        public void Report(MetricInfo info)
        {
            Last = info;
        }
    }
}
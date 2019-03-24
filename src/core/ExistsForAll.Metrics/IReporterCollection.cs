using System.Collections.Generic;

namespace ExistsForAll.Metrics
{
    public interface IReporterCollection : IEnumerable<IMetricReporter>
    {
        void AddReporter(IMetricReporter metricReporter);
        void AddReporters(IEnumerable<IMetricReporter> merticReporters);
        void ClearReporters();
    }
}

using System.Collections.Generic;

namespace ExistsForAll.Metrics
{
    internal class ReporterCollection : List<IMetricReporter>, IReporterCollection
    {
        public void AddReporter(IMetricReporter metricReporter)
        {
            Add(metricReporter);
        }

        public void AddReporters(IEnumerable<IMetricReporter> merticReporters)
        {
            AddRange(merticReporters);
        }

        public void ClearReporters()
        {
            Clear();
        }
    }
}

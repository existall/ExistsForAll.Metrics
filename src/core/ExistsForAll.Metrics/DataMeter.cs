using System.Collections.Generic;

namespace ExistsForAll.Metrics
{
    public class DataMeter
    {
        private readonly Meter _meter;
        private readonly MetricInfo _metricInfo = new MetricInfo();

        public IDictionary<string, object> Data => _metricInfo.Data;

        internal DataMeter(Meter meter)
        {
            _meter = meter;
        }

        public void Send()
        {
            _meter.Send(_metricInfo);
        }
    }
}
using System.Collections.Generic;

namespace ExistsForAll.Metrics
{
    public class MetricInfo
    {
        public string Key { get; internal set; }

        public IDictionary<string, object> Data { get; }

        public MetricInfo(IDictionary<string, object> data)
        {
            Data = data;
        }

        public MetricInfo()
        {
            Data = new Dictionary<string, object>();
        }
    }
}

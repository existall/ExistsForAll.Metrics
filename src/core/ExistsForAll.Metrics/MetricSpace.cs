namespace ExistsForAll.Metrics
{
    public class MetricSpace
    {
        private readonly string _key;

        public MetricsOptions Options { get; }

        public MetricSpace(string key)
        {
            _key = key;
            Options = new MetricsOptions();
        }

        internal MetricSpace(string key, MetricsOptions options)
        {
            _key = key;
            Options = options;
        }

        public MetricSpace Space(string key)
        {
            return new MetricSpace(JoinKeys(key), Options);
        }

        public Meter Meter(string key)
        {
            return new Meter(JoinKeys(key), Options);
        }

        private string JoinKeys(string key)
        {
            return $"{_key}{Options.KeysDelimiter}{key}";
        }
    }
}

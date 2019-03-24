namespace ExistsForAll.Metrics
{
    public class Meter
    {
        private MetricsOptions Options { get; }

        private string Key { get; }

        internal Meter(string key, MetricsOptions metricsOptions)
        {
            Options = metricsOptions;

            Key = Options.KeyFormater.Format(key);
        }

        public void Send(MetricInfo metricInfo)
        {
            if(!Options.ReportingState.IsEnabled())
                return;

            metricInfo.Key = Key;

            foreach (var reporter in Options.Reporters)
            {
                reporter.Report(metricInfo);
            }
        }
    }
}

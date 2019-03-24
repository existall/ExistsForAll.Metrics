namespace ExistsForAll.Metrics
{
    public class MetricsOptions
    {
        public ISystemClock SystemClock { get; set; } = new SystemClock();

        internal IReportingState ReportingState { get; }

        public IReporterCollection Reporters { get; } = new ReporterCollection();

        public IKeyFormater KeyFormater { get; set; } = new DefaultKeyFormater();

        public string KeysDelimiter { get; set; } = ".";

        internal MetricsOptions(IReportingState reportingState = null)
        {
            ReportingState = reportingState ?? new ReportingState(this);
        }
    }
}

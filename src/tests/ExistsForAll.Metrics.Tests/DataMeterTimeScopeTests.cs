using System;
using NSubstitute;
using Xunit;

namespace ExistsForAll.Metrics.Tests
{
    public class DataMeterTimeScopeTests
    {
        private const string DurationKey = "time-elapsed";

        [Fact]
        public void DateMeterTimeScope_WhenUsingTimeScope_ShouldSendExecutionTime()
        {
            var sut = new MetricSpace("s");

            var recorder = new ReportsRecorder();

            sut.AddReporter(recorder);

            var meter = sut.DataMeter("x");

            meter.AddData("some-value", "value");

            using (meter.TimeScope(DurationKey))
            {

            }

            Assert.Contains(DurationKey, recorder.Last.Data.Keys);
            Assert.IsType<double>(recorder.Last.Data[DurationKey]);
        }

        [Fact]
        public void DateMeterTimeScope_WhenUsingTimeScopeAction_ShouldSendExecutionTime()
        {
            var sut = new MetricSpace("s");

            var recorder = new ReportsRecorder();

            sut.AddReporter(recorder);

            var meter = sut.DataMeter("x");

            meter.Data.Add("some-value", "value");

            meter.TimeScope(() => { }, DurationKey);

            Assert.Contains(DurationKey, recorder.Last.Data.Keys);
            Assert.IsType<double>(recorder.Last.Data[DurationKey]);
        }

        [Fact]
        public void DataMeterConditionalTimeScope_WhenConditionIsTrue_ShouldSendExecutionTime()
        {
            var sut = new MetricSpace("s");

            var recorder = new ReportsRecorder();

            sut.AddReporter(recorder);

            var meter = sut.DataMeter("x");

            meter.Data.Add("some-value", "value");

            using (meter.ConditionalTimeScope(() => true, DurationKey))
            {

            }

            Assert.Contains(DurationKey, recorder.Last.Data.Keys);
            Assert.IsType<double>(recorder.Last.Data[DurationKey]);
        }

        [Fact]
        public void DataMeterConditionalTimeScope_WhenConditionIsFalse_ShouldNotSendExecutionTime()
        {
            var sut = new MetricSpace("s");

            var metricReporter = Substitute.For<IMetricReporter>();

            sut.AddReporter(metricReporter);

            var meter = sut.DataMeter("x");

            meter.Data.Add("some-value", "value");

            using (meter.ConditionalTimeScope(() => false, DurationKey))
            {

            }

            metricReporter.DidNotReceive().Report(Arg.Any<MetricInfo>());
        }

        [Fact]
        public void DataMemberActionConditionalTimeScope_WhenConditionIsTrue_ShouldSendExecutionTime()
        {
            var sut = new MetricSpace("s");

            var recorder = new ReportsRecorder();

            sut.AddReporter(recorder);

            var meter = sut.DataMeter("x");

            meter.Data.Add("some-value", "value");

            meter.ConditionalTimeScope(() => { }, () => true, DurationKey);

            Assert.Contains(DurationKey, recorder.Last.Data.Keys);
            Assert.IsType<double>(recorder.Last.Data[DurationKey]);
        }

        [Fact]
        public void DataMeterActionConditionalTimeScope_WhenConditionIsFalse_ShouldNotSendExecutionTime()
        {
            var sut = new MetricSpace("s");

            var metricReporter = Substitute.For<IMetricReporter>();

            sut.AddReporter(metricReporter);

            var meter = sut.DataMeter("x");

            meter.Data.Add("some-value", "value");

            meter.ConditionalTimeScope(() => { }, () => false, DurationKey);

            metricReporter.DidNotReceive().Report(Arg.Any<MetricInfo>());
        }
    }
}

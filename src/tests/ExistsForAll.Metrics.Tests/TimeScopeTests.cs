using System;
using NSubstitute;
using Xunit;

namespace ExistsForAll.Metrics.Tests
{
    public class TimeScopeTests
    {
        private const string DurationKey = "time-elapsed";

        [Fact]
        public void TimeScope_WhenUsingTimeScope_ShouldSendExecutionTime()
        {
            var sut = new MetricSpace("s");

            var recorder = new ReportsRecorder();

            sut.AddReporter(recorder);

            var meter = sut.Meter("x");

            var info = new MetricInfo();

            info.Data.Add("some-value", "value");

            using (meter.TimeScope(info, DurationKey))
            {

            }

            Assert.Contains(DurationKey, recorder.Last.Data.Keys);
            Assert.IsType<TimeSpan>(recorder.Last.Data[DurationKey]);
        }

        [Fact]
        public void TimeScope_WhenUsingTimeScopeAction_ShouldSendExecutionTime()
        {
            var sut = new MetricSpace("s");

            var recorder = new ReportsRecorder();

            sut.AddReporter(recorder);

            var meter = sut.Meter("x");

            var info = new MetricInfo();

            info.Data.Add("some-value", "value");

            meter.TimeScope(info, () => { }, DurationKey);

            Assert.Contains(DurationKey, recorder.Last.Data.Keys);
            Assert.IsType<TimeSpan>(recorder.Last.Data[DurationKey]);
        }

        [Fact]
        public void ConditionalTimeScope_WhenConditionIsTrue_ShouldSendExecutionTime()
        {
            var sut = new MetricSpace("s");

            var recorder = new ReportsRecorder();

            sut.AddReporter(recorder);

            var meter = sut.Meter("x");

            var info = new MetricInfo();

            info.Data.Add("some-value", "value");

            using (meter.ConditionalTimeScope(info, () => true, DurationKey))
            {

            }

            Assert.Contains(DurationKey, recorder.Last.Data.Keys);
            Assert.IsType<TimeSpan>(recorder.Last.Data[DurationKey]);
        }

        [Fact]
        public void ConditionalTimeScope_WhenConditionIsFalse_ShouldNotSendExecutionTime()
        {
            var sut = new MetricSpace("s");

            var metricReporter = Substitute.For<IMetricReporter>();

            sut.AddReporter(metricReporter);

            var meter = sut.Meter("x");

            var info = new MetricInfo();

            info.Data.Add("some-value", "value");

            using (meter.ConditionalTimeScope(info, () => false, DurationKey))
            {

            }

            metricReporter.DidNotReceive().Report(Arg.Any<MetricInfo>());
        }

        [Fact]
        public void ActionConditionalTimeScope_WhenConditionIsTrue_ShouldSendExecutionTime()
        {
            var sut = new MetricSpace("s");

            var recorder = new ReportsRecorder();

            sut.AddReporter(recorder);

            var meter = sut.Meter("x");

            var info = new MetricInfo();

            info.Data.Add("some-value", "value");

            meter.ConditionalTimeScope(info, () => { }, () => true, DurationKey);

            Assert.Contains(DurationKey, recorder.Last.Data.Keys);
            Assert.IsType<TimeSpan>(recorder.Last.Data[DurationKey]);
        }

        [Fact]
        public void ActionConditionalTimeScope_WhenConditionIsFalse_ShouldNotSendExecutionTime()
        {
            var sut = new MetricSpace("s");

            var metricReporter = Substitute.For<IMetricReporter>();

            sut.AddReporter(metricReporter);

            var meter = sut.Meter("x");

            var info = new MetricInfo();

            info.Data.Add("some-value", "value");

            meter.ConditionalTimeScope(info, () => { }, () => false, DurationKey);

            metricReporter.DidNotReceive().Report(Arg.Any<MetricInfo>());
        }
    }
}

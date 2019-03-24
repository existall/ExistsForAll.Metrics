using System;
using ExistsForAll.Metrics;
using Xunit;

namespace ExistsForAll.Metrics.Tests
{
    public class ReportingStateTests
    {
        [Fact]
        public void IsEnabled_WhenNewingupNewSpace_ShouldReturnTrue()
        {
            var sut = new MetricSpace("ss");

            var result = sut.IsEnabled();

            Assert.True(result);
        }

        [Fact]
        public void IsEnabled_WhenDisabed_ShouldReturnFasle()
        {
            var sut = new MetricSpace("ss");

            sut.Disable();

            var result = sut.IsEnabled();

            Assert.False(result);
        }

        [Fact]
        public void IsEnabled_WhenSuspended_ShouldReturnFasle()
        {
            var sut = new MetricSpace("ss");

            var testClock = new TestClock();

            sut.Options.SystemClock = testClock;

            var currentTime = new DateTime(2000, 1, 1);

            testClock.TestTime = currentTime;

            sut.Suspend(TimeSpan.FromHours(1));

            var result = sut.IsEnabled();

            Assert.False(result);
        }

        [Fact]
        public void IsEnabled_WhenSuspendedWhenTimeHasNotPassed_ShouldBeDisabled()
        {
            var sut = new MetricSpace("ss");

            var testClock = new TestClock();

            sut.Options.SystemClock = testClock;

            var currentTime = new DateTime(2000, 1, 1);

            testClock.TestTime = currentTime;

            sut.Suspend(TimeSpan.FromHours(1));

            testClock.TestTime = new DateTime(2000, 1, 1, 00, 30, 00);

            var result = sut.IsEnabled();

            Assert.False(result);
        }

        [Fact]
        public void IsEnabled_WhenSuspendedWhenTimeHasPassed_ShouldBeEnabled()
        {
            var sut = new MetricSpace("ss");

            var testClock = new TestClock();

            sut.Options.SystemClock = testClock;

            var currentTime = new DateTime(2000, 1, 1);

            testClock.TestTime = currentTime;

            sut.Suspend(TimeSpan.FromHours(1));

            testClock.TestTime = new DateTime(2000, 1, 1, 1, 00, 00);

            var result = sut.IsEnabled();

            Assert.True(result);
        }

        [Fact]
        public void IsEnabled_WhenEnableAfterDisable_ShouldBeEnabled()
        {
            var sut = new MetricSpace("ss");

            sut.IsEnabled();

            AssertEnabled(sut);

            sut.Disable();

            AssertDisabled(sut);

            sut.Enable();

            AssertEnabled(sut);
        }

        private void AssertEnabled(MetricSpace metricSpace)
        {
            var result = metricSpace.IsEnabled();

            Assert.True(result);
        }

        private void AssertDisabled(MetricSpace metricSpace)
        {
            var result = metricSpace.IsEnabled();

            Assert.False(result);
        }
    }
}

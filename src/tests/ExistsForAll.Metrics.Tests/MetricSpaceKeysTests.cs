using ExistsForAll.Metrics;
using Xunit;

namespace ExistsForAll.Metrics.Tests
{
    public class MetricSpaceKeysTests
    {
        private const string Key1 = "key1";
        private const string Key2 = "key2";
        private const string Key3 = "key3";

        [Fact]
        public void MetricSpace_WhenCombiningKeys_ShouldMatchPattern()
        {
            var sut = new MetricSpace(Key1);

            var metricInfo = new MetricInfo();

            sut.Meter(Key2).Send(metricInfo);

            var result = metricInfo.Key;

            Assert.Equal($"{Key1}.{Key2}", result);
        }

        [Fact]
        public void MetricSpace_WhenCombiningThreeKeys_ShouldMatchPattern()
        {
            var space = new MetricSpace(Key1);

            var sut = space.Space(Key2);

            var metricInfo = new MetricInfo();

            sut.Meter(Key3).Send(metricInfo);

            var result = metricInfo.Key;

            Assert.Equal($"{Key1}.{Key2}.{Key3}", result);
        }

        [Fact]
        public void MetricSpace_WhenUsingDifferntDelimiter_ShouldMatchPattern()
        {
            const string delimiter = ";";

            var sut = new MetricSpace(Key1);

            sut.Options.KeysDelimiter = delimiter;

            var metricInfo = new MetricInfo();

            sut.Meter(Key2).Send(metricInfo);

            var result = metricInfo.Key;

            Assert.Equal($"{Key1}{delimiter}{Key2}", result);
        }

        [Fact]
        public void MetricSpace_WhenUsingDifferntDelimiterOnMultiSpace_ShouldMatchPattern()
        {
            const string delimiter = ";";

            var space = new MetricSpace(Key1);

            space.Options.KeysDelimiter = delimiter;

            var sut = space.Space(Key2);

            var metricInfo = new MetricInfo();

            sut.Meter(Key3).Send(metricInfo);

            var result = metricInfo.Key;

            Assert.Equal($"{Key1}{delimiter}{Key2}{delimiter}{Key3}", result);
        }

        [Fact]
        public void MetricSpace_WhenReplcingKeyFormater_ShouldMatchPattern()
        {
            var space = new MetricSpace(Key1);

            space.Options.KeyFormater = new TestFormatter();

            var sut = space.Space(Key2);

            var metricInfo = new MetricInfo();

            sut.Meter(Key3).Send(metricInfo);

            var result = metricInfo.Key;

            Assert.Equal(TestFormatter.Key, result);
        }

        private class TestFormatter : IKeyFormater
        {
            public const string Key = "some-long-key";

            public string Format(string key)
            {
                return Key;
            }
        }
    }
}

using System;

namespace ExistsForAll.Metrics
{
    public interface IKeyFormater
    {
        string Format(string key);
    }

    internal class DefaultKeyFormater : IKeyFormater
    {
        public string Format(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return key;
        }
    }
}

using System;

namespace ExistsForAll.Metrics
{
    internal class ExecutionScope : IDisposable
    {
        private readonly Action _onDispose;

        public ExecutionScope(Action onStart, Action onDispose)
        {
            _onDispose = onDispose;

            onStart?.Invoke();
        }

        public void Dispose()
        {
            _onDispose?.Invoke();
        }
    }
}
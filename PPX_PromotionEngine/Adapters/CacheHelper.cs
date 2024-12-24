using System;
using System.Threading;

namespace PPX_PromotionEngine
{
    public class CacheHelper<T> : IDisposable
    {
        private const int CacheDurationMinutes = 5;
        private readonly Func<T> _updateFunction;
        private T _cachedValue;
        private readonly Timer _timer;

        public CacheHelper(Func<T> updateFunction)
        {
            _updateFunction = updateFunction ?? throw new ArgumentNullException(nameof(updateFunction));
            _cachedValue = _updateFunction(); //init cache

            _timer = new Timer(UpdateCache, null, TimeSpan.Zero, TimeSpan.FromMinutes(CacheDurationMinutes));
        }

        public T GetCachedValue()
        {
            return _cachedValue;
        }

        private void UpdateCache(object state)
        {
            try
            {
                _cachedValue = _updateFunction();
            }
            catch (Exception ex)
            {
                //log
            }
        }

        public void Dispose()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            _timer?.Dispose();
        }

        ~CacheHelper()
        {
            Dispose();
        }
    }
}
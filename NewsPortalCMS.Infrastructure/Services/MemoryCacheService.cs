using Microsoft.Extensions.Caching.Memory;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Infrastructure.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        private readonly HashSet<string> _keys = new();

        private readonly object _lock = new();

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void TrackKey(string key)
        {
            lock (_lock)
            {
                _keys.Add(key);
            }
        }

        public void Remove(string key)
        {
            _cache.Remove(key);

            lock (_lock)
            {
                _keys.Remove(key);
            }
        }

        public void RemoveByPrefix(string prefix)
        {
            List<string> keysToRemove;

            lock (_lock)
            {
                keysToRemove = _keys
                    .Where(k => k.StartsWith(
                        prefix,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);

                lock (_lock)
                {
                    _keys.Remove(key);
                }
            }
        }
    }
}
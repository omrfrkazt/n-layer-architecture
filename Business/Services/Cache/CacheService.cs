using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Services.Cache
{
    public partial class CacheService : ICacheService
    {
        #region Fields&Ctor
        private bool _disposed;

        private readonly IMemoryCache _memoryCache;

        private static readonly ConcurrentDictionary<string, CancellationTokenSource> _prefixes = new ConcurrentDictionary<string, CancellationTokenSource>();
        private static CancellationTokenSource _clearToken = new CancellationTokenSource();

        private readonly AppSettings _appSettings;
        public CacheService(IMemoryCache memoryCache,
            IOptions<AppSettings> appSettings)
        {
            _memoryCache = memoryCache;
            _appSettings = appSettings.Value;
        }
        #endregion


        #region Methods

        public T Get<T>(string key, Func<T> acquire)
        {
            if (_appSettings.CacheExpiresMunites <= 0)
                return acquire();

            var result = _memoryCache.GetOrCreate(key, entry =>
            {
                entry.SetOptions(PrepareEntryOptions(key));

                return acquire();
            });

            //do not cache null value
            if (result == null)
                Remove(key);

            return result;
        }


        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }


        public async Task<T> GetAsync<T>(string key, Func<Task<T>> acquire)
        {
            if (_appSettings.CacheExpiresMunites <= 0)
                return await acquire();

            var result = await _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.SetOptions(PrepareEntryOptions(key));

                return await acquire();
            });

            //do not cache null value
            if (result == null)
                Remove(key);

            return result;
        }


        public void Set(string key, object data)
        {
            if (_appSettings.CacheExpiresMunites <= 0 || data == null)
                return;

            _memoryCache.Set(key, data, PrepareEntryOptions(key));
        }


        public bool IsSet(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }


        public void Clear()
        {
            _clearToken.Cancel();
            _clearToken.Dispose();

            _clearToken = new CancellationTokenSource();

            foreach (var prefix in _prefixes.Keys.ToList())
            {
                _prefixes.TryRemove(prefix, out var tokenSource);
                tokenSource?.Dispose();
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _memoryCache.Dispose();
            }

            _disposed = true;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Prepare cache entry options for the passed key
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <returns>Cache entry options</returns>
        private MemoryCacheEntryOptions PrepareEntryOptions(string key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            //set expiration time for the passed cache key
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_appSettings.CacheExpiresMunites)
            };
            options.AddExpirationToken(new CancellationChangeToken(_clearToken.Token));
            return options;
        }

        #endregion
    }
}

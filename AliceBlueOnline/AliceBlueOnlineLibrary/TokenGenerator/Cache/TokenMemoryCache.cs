using AliceBlueOnlineLibrary.Abstractions;
using System;
using System.Reflection;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace AliceBlueOnlineLibrary.TokenGenerator.Cache
{
    public class TokenMemoryCache<T> : ITokenMemoryCache<T> where T : class
    {
        private readonly MemoryCache _cache;

        public TokenMemoryCache()
        {
            _cache = new MemoryCache("TokenCaching");
        }

        /// <inheritdoc />
        public async Task<T> GetTokenFromCache(string cacheKey, Func<Task<T>> func)
        {
            if (_cache.Get(cacheKey) is T tokenResponse)
            {
                return tokenResponse;
            }

            tokenResponse = await func().ConfigureAwait(false);

            if (!(typeof(T).GetProperty("ExpirationDelay", BindingFlags.Instance | BindingFlags.Public)
                ?.GetValue(tokenResponse) is int expirationDelay))
            {
                return tokenResponse;
            }

            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(expirationDelay)
            };

            _cache.Add(new CacheItem(cacheKey, tokenResponse), cacheItemPolicy);

            return tokenResponse;
        }
    }
}

using AliceBlueOnlineLibrary.TokenGenerator.Response;
using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace AliceBlueOnlineLibrary.TokenGenerator.Cache
{
    public class TokenMemoryCache
    {
        private readonly MemoryCache _cache;

        public TokenMemoryCache()
        {
            _cache = new MemoryCache("TokenCaching");
        }

        public async Task<string> GetTokenFromCache(string cacheKey, Func<Task<TokenResponse>> func)
        {
            TokenResponse tokenResponse = _cache.Get(cacheKey) as TokenResponse;
            if (tokenResponse == null)
            {
                tokenResponse = await func().ConfigureAwait(false);

                CacheItem cacheItem = new CacheItem(cacheKey, tokenResponse);
                var cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(tokenResponse.ExpirationDelay)
                };

                _cache.Add(cacheItem, cacheItemPolicy);
            }

            return tokenResponse.AccessToken;
        }
    }
}

using AliceBlueOnlineLibrary.DataContract.Token.Response;
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
            if (_cache.Get(cacheKey) is TokenResponse tokenResponse)
            {
                return tokenResponse.AccessToken;
            }

            tokenResponse = await func().ConfigureAwait(false);

            var cacheItem = new CacheItem(cacheKey, tokenResponse);
            var cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(tokenResponse.ExpirationDelay)
            };

            _cache.Add(cacheItem, cacheItemPolicy);

            return tokenResponse.AccessToken;
        }
    }
}

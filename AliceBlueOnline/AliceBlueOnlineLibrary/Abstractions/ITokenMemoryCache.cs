using System;
using System.Threading.Tasks;

namespace AliceBlueOnlineLibrary.Abstractions
{
    public interface ITokenMemoryCache<T>
    {
        /// <summary>
        /// Gets the token from cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="func">The function.</param>
        /// <returns>The tone.</returns>
        Task<T> GetTokenFromCache(string cacheKey, Func<Task<T>> func);
    }
}

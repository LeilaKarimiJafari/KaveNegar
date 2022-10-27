using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KarimiProject.RedisService.Extension
{
    public static class DistributedCashExtensions
    {
        public static async Task SetDistributedCache<T>(this IDistributedCache cache, string recordID, T dataEntry,
          TimeSpan? absolutExpirationTime = null,
          TimeSpan? unUsedExpirationTime = null)
        {
            var casheOption = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = absolutExpirationTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = unUsedExpirationTime
            };

            var entryJson = JsonSerializer.Serialize(dataEntry);
            await cache.SetStringAsync(recordID, entryJson, casheOption);
        }

        public static async Task<T> GetDistributedCashe<T>(this IDistributedCache cache, string recordID)
        {
            var jsonResult = await cache.GetStringAsync(recordID);

            if (jsonResult is null)
                return default(T);
            else
            {
                var result = JsonSerializer.Deserialize<T>(jsonResult);
                return result;
            }
        }
    }
}

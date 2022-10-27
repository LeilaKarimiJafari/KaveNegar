using KarimiProject.Entities;
using KarimiProject.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KarimiProject.RedisService.Extension;

namespace KarimiProject.RedisService
{
    public class RedisDataService : IDataService
    {
        private readonly IDistributedCache distributedCache;

        public RedisDataService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }
        public async Task SetCustomersInDataBase(string fileName, List<Customer> customers)
        {
            long cachKeyIndex = 0;
            foreach (var customer in customers)
            {
                string key = fileName + cachKeyIndex++;
                await Task.Run(async () => await distributedCache.SetDistributedCache<Customer>(key, customer, TimeSpan.FromHours(1)));
            }
        }
    }
}

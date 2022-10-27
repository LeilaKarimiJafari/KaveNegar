using KarimiProject.Entities;
using KarimiProject.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpfApplication.Extension;

namespace WpfApplication.Classes
{
    public class RedisCache : IDestributedCache
    {
        private readonly IDistributedCache cache;

        public RedisCache(IDistributedCache cache)
        {
            this.cache = cache;
        }
        public async Task<List<Customer>> GetCustomersFromCache(string fileName)
        {
            int i = 1;
            List<Customer> customers = new List<Customer>();
            do
            {
                var customer = await cache.GetDistributedCashe<Customer>(fileName + i++);
                if (customer == default(Customer))
                    break;
                else
                    customers.Add(customer);

            } while (true);

            return customers;

        }

        public async Task SetCustomersInCache(string fileName, List<Customer> customers)
        {
            int i = 1;
            foreach (var customer in customers)
            {
                await cache.SetDistributedCache<Customer>(fileName + i++, customer, TimeSpan.FromHours(1));
            }
        }
    }
}
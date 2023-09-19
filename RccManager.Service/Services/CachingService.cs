using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Services;
using RccManager.Service.Helper;
using StackExchange.Redis;

namespace RccManager.Service.Services
{
    public class CachingService : ICachingService
    {
        private readonly IDatabase database;

        public CachingService(IDistributedCache cache)
        {
            this.database = RedisHelper.GetDatabase();
            
        }

        public async Task<string> GetAsync(string hash, string key)
        {
            return await database.HashGetAsync(hash, key);
        }

        public async Task SetAsync(string hash, string key, string value)
        {
            await database.HashSetAsync(hash, key, value);
        }

        public async Task<HashEntry[]> GetAllAsync(string hash)
        {
            return await database.HashGetAllAsync(hash);
        }

        public async Task DeleteAsync(string hash)
        {
            await database.KeyDeleteAsync(hash);
        }
    }
}


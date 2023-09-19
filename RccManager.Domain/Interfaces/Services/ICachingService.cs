using RccManager.Domain.Entities;
using StackExchange.Redis;

namespace RccManager.Domain.Interfaces.Services
{
    public interface ICachingService
	{
        Task SetAsync(string hash, string key, string value);
        Task<string> GetAsync(string hash, string key);
        Task<HashEntry[]> GetAllAsync(string hash);
        Task DeleteAsync(string hash);

    }
}


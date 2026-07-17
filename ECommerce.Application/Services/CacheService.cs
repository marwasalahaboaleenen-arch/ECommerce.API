using ECommerce.Application.Contracts;
using ECommerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepo;

        public CacheService(ICacheRepository cacheRepo)
        {
            _cacheRepo = cacheRepo;
        }
        public async Task<string?> GetDataAsync(string cacheKey, CancellationToken ct = default)
     =>await _cacheRepo.GetAsync(cacheKey, ct);

        public async Task SetDataAsync(string cacheKey, object cacheValue, TimeSpan? TimeToLive = null, CancellationToken ct = default)
        {
           var jsonValue = JsonSerializer.Serialize(cacheValue);
            await _cacheRepo.SetAsync(cacheKey, jsonValue, TimeToLive, ct);
        }

    
    }
}

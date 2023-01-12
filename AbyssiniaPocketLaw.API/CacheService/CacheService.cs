namespace AbyssiniaPocketLaw.API.CacheService;
using AbyssiniaPocketLaw.API.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

public class CacheService : ICacheService
{
    private readonly PocketLawDbContext _dbContext;
    private IMemoryCache _cache;
    private InMemoryCacheSetting _cacheSetting;

    public CacheService(PocketLawDbContext dbContext, IMemoryCache cache, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _cache = cache;
        _cacheSetting = configuration
               .GetSection("InMemoryCacheSetting")
               .Get<InMemoryCacheSetting>();
    }
    public async Task<IEnumerable<T>> GetData<T>(string cacheKey) where T : class
    {
        IEnumerable<T>? result = null;

        if (!_cacheSetting.IsCachingEnabled)
        {
            return _dbContext.Set<T>().AsEnumerable();
        }
       
        if (!_cache.TryGetValue(cacheKey, out result) || result == null)
          {
                result = await _dbContext.Set<T>().ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromHours(_cacheSetting.SlidingExpiration))
                                    .SetAbsoluteExpiration(TimeSpan.FromHours(_cacheSetting.AbsoluteExpiration))
                                    .SetPriority(CacheItemPriority.Normal)
                                    .SetSize(_cacheSetting.Size);
                _cache.Set(cacheKey, result, cacheEntryOptions);
          }
        
        return result;
    }
}

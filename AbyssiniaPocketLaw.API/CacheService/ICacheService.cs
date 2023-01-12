namespace AbyssiniaPocketLaw.API.CacheService;

public interface ICacheService
{
    Task<IEnumerable<T>> GetData<T>(string cacheKey) where T : class;
}

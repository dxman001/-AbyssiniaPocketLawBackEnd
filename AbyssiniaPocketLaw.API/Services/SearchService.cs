namespace AbyssiniaPocketLaw.API.Services;

using AbyssiniaPocketLaw.API.CacheService;
using AbyssiniaPocketLaw.API.Entities;
using System.Linq;

public class SearchService : ISearchService
{
    
    private ICacheService _cacheService;
    const int pageSize = 20;
    private const string lawsCacheKey = "lawsCacheKey";
    private const string cassationCacheKey = "cassationCacheKey";
    
    public SearchService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }
    public async Task<(IEnumerable<object>? data,int count)> Search(string searchKey, string? searchType, int pageIndex = 0)
    {
        int skipValue = pageIndex * pageSize;
        IEnumerable<object> result = searchType switch
        {
            "Cassations" => await SearchCassations(searchKey),
            _ => await SearchLaws(searchKey)
        };

        return (result.Skip(skipValue).Take(pageSize).ToList(), result.Count());
    }

    private async Task<IEnumerable<Law>> SearchLaws(string searchKey)
    {
        IEnumerable<Law> lawsList = await _cacheService.GetData<Law>(lawsCacheKey);
        return lawsList.Where(l =>
                   l.Title.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Status.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Entry.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Category.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Jurisdiction.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Keywords.Contains(searchKey, StringComparison.OrdinalIgnoreCase));
    }

    private async Task<IEnumerable<Cassation>> SearchCassations(string searchKey)
    {
        IEnumerable<Cassation> cassationsList = await _cacheService.GetData<Cassation>(cassationCacheKey); ;
        return cassationsList.Where(l =>
                      l.Title.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                      l.Volume.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                      l.Decision.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                      l.Category.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                      l.Given.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                      l.Keywords.Contains(searchKey, StringComparison.OrdinalIgnoreCase));
    }

}

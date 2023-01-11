namespace AbyssiniaPocketLaw.API.Services;
using AbyssiniaPocketLaw.API.Entities;
using AbyssiniaPocketLaw.API.Persistance;

public class SearchService : ISearchService
{
    private readonly PocketLawDbContext _dbContext;
    const int pageSize = 20;
    public SearchService(PocketLawDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public (IEnumerable<BaseEntity>? data,int count) Search(string searchKey, string? searchType, int pageIndex = 0)
    {
        int skipValue = pageIndex * pageSize;
        var result = searchType switch
        {
            "Cassations" => SearchCassations(searchKey),
            _ => SearchLaws(searchKey)
        };

        return (result.Skip(skipValue).Take(pageSize).ToList(), result.Count());
    }

    private IEnumerable<BaseEntity> SearchCassations(string searchKey) =>
        _dbContext.Cassations.AsEnumerable()
                 .Where(l =>
                        l.Title.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                        l.Volume.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                        l.Decision.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                        l.Category.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                        l.Given.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                        l.Keywords.Contains(searchKey, StringComparison.OrdinalIgnoreCase));

    private IEnumerable<BaseEntity> SearchLaws(string searchKey) =>
        _dbContext.Laws.AsEnumerable()
                .Where(l =>
                   l.Title.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Status.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Entry.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Category.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Jurisdiction.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Keywords.Contains(searchKey, StringComparison.OrdinalIgnoreCase));

}

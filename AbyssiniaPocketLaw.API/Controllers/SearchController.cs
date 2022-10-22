namespace AbyssiniaPocketLaw.API.Controllers;

using AbyssiniaPocketLaw.API.DTOs;
using AbyssiniaPocketLaw.API.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly PocketLawDbContext _dbContext;
    const int pageSize = 20;
    public SearchController(PocketLawDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ApiResponse> SearchLaws(string? searchKey = null, string searchType = "Laws", int pageIndex = 0)
    {
        int skipValue = pageIndex * pageSize;
        if (searchKey != null && !string.IsNullOrWhiteSpace(searchKey))
        {
            if (searchType.Equals("Cassations"))
            {
                var result = _dbContext.Cassations.AsEnumerable()
                .Where(l =>
                       l.Title.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                       l.Volume.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                       l.Decision.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                       l.Category.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                       l.Given.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                       l.Keywords.Contains(searchKey, StringComparison.OrdinalIgnoreCase));

                return new ApiResponse(result.Skip(skipValue)
                                        .Take(pageSize)
                                        .ToList(),
                                        result.Count());
            }
            else
            {
                var result = _dbContext.Laws.AsEnumerable()
                .Where(l =>
                   l.Title.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Status.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Entry.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Category.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Jurisdiction.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Keywords.Contains(searchKey, StringComparison.OrdinalIgnoreCase));

                return new ApiResponse(result.Skip(skipValue)
                                         .Take(pageSize)
                                         .ToList() , 
                                        result.Count());
            }

        }

        return new ApiResponse(); ;
    }
}

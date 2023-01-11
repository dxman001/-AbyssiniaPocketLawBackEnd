namespace AbyssiniaPocketLaw.API.Controllers;

using AbyssiniaPocketLaw.API.DTOs;
using AbyssiniaPocketLaw.API.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;
    public SearchController(ISearchService searchService)
    {
      _searchService = searchService;
    }

    [HttpGet]
    public ApiResponse SearchLaws(string? searchKey = null, string? searchType = "Laws", int pageIndex = 0)
    {
        if (searchKey != null && !string.IsNullOrWhiteSpace(searchKey))
        {
            var result = _searchService.Search(searchKey, searchType!, pageIndex);
            return new ApiResponse(result.data, result.count);
        }
        return new ApiResponse();
    }
}

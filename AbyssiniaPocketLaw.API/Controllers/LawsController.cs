namespace AbyssiniaPocketLaw.API.Controllers;

using AbyssiniaPocketLaw.API.Entities;
using AbyssiniaPocketLaw.API.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class LawsController : ControllerBase
{
    private readonly PocketLawDbContext _dbContext;
    const int pageSize = 20;
    public LawsController(PocketLawDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public List<Law> SearchLaws(string? searchKey = null,string? searchType= "Law", int pageIndex = 0)
    {
        int skipValue = pageIndex * pageSize;
        if(searchKey != null && !string.IsNullOrWhiteSpace(searchKey))
        {
           return _dbContext.Laws.AsEnumerable()
                .Where(l => 
                   l.Title.Contains(searchKey, StringComparison.OrdinalIgnoreCase) || 
                   l.Status.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Entry.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Category.Contains(searchKey, StringComparison.OrdinalIgnoreCase) ||
                   l.Jurisdiction.Contains(searchKey, StringComparison.OrdinalIgnoreCase) || 
                   l.Keywords.Contains(searchKey, StringComparison.OrdinalIgnoreCase))
                .Skip(skipValue)
                .Take(pageSize)
                .ToList();
        }
        
        return _dbContext.Laws.Skip(skipValue).Take(pageSize).ToList();
    }
}

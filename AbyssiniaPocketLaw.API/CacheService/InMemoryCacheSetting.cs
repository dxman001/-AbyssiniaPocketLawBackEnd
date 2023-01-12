namespace AbyssiniaPocketLaw.API.CacheService;

public class InMemoryCacheSetting
{
    public bool IsCachingEnabled { get; set; }
    public int SlidingExpiration { get; set; }
    public int AbsoluteExpiration { get; set; }
    public int Priority { get; set; }
    public int Size { get; set; }
}

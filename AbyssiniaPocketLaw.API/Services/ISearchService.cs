﻿namespace AbyssiniaPocketLaw.API.Services;
using AbyssiniaPocketLaw.API.Entities;

public interface ISearchService
{
   Task<(IEnumerable<object>? data, int count)> Search(string searchKey, string searchType, int pageIndex = 0);
}

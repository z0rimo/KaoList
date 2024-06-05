// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Models.Searches;

namespace CodeRabbits.KaoList.Web.Services.Searches
{
    public interface ISearchService
    {
        Task<SearchListResponse> SearchAsync(IEnumerable<string> queries, int offset, int maxResults);
    }
}

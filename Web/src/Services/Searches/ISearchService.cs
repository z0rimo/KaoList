// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Models.Searches;

namespace CodeRabbits.KaoList.Web.Services.Searches
{
    public interface ISearchService
    {
        Task<SongSearchListResponse> SongSearchAsync(string query, int offset, int maxResults);
    }
}

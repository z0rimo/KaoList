// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web
{
    public static class PaginationHelper
    {
        public static (int? NextPageToken, int? PrevPageToken) CalculatePageTokens(int offset, int maxResults, int totalResults)
        {
            int? nextOffset = offset + maxResults < totalResults ? offset + maxResults : null;
            int? prevOffset = offset > 0 ? Math.Max(offset - maxResults, 0) : null;
            return (NextPageToken: nextOffset, PrevPageToken: prevOffset);
        }
    }
}

// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models
{
    public abstract class KaoListPageResponse : KaoListResponse
    {
        public virtual string? NextPageToken { get; set; }

        public virtual string? PrevPageToken { get; set; }

        public virtual PageInfo? PageInfo { get; set; }
    }
}

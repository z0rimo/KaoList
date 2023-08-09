// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.Thumbnails
{
    public class ThumbnailSetResponse : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#thumbnailSetResponse";

        public IEnumerable<ThumbnailResource>? Items { get; set; }
    }
}

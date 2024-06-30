// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Song;
using CodeRabbits.KaoList.Web.Models.Songs;
using CodeRabbits.KaoList.Web.Models.Thumbnails;

namespace CodeRabbits.KaoList.Web.Models.Searches
{
    public class SongSearchSnippet
    {

        public virtual string? SingId { get; set; } = default!;

        public virtual string? Title { get; set; } = default!;

        public virtual IEnumerable<SongUser>? Artists { get; set; } = default!;

        public virtual DateTime? Created { get; set; } = default!;

        public virtual ThumbnailResource? Thumbnail { get; set; } = default!;

        public virtual IEnumerable<Karaoke>? Karaokes { get; set; } = default!;

        public virtual bool? IsLiked { get; set; } = default!;
    }
}

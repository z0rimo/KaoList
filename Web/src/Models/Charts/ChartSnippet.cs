// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Song;
using CodeRabbits.KaoList.Web.Models.Songs;
using CodeRabbits.KaoList.Web.Models.Thumbnails;

namespace CodeRabbits.KaoList.Web.Models.Charts
{
    public class ChartSnippet
    {
        public virtual ThumbnailResource? Thumbnail { get; set; } = default!;

        public virtual string? Title { get; set; } = default!;

        public virtual IEnumerable<ChartUser> SingUsers { get; set; } = default!;

        public virtual string? Composer { get; set; } = default!;

        public virtual DateTime? Created { get; set; } = default!;

        public virtual SongKaraokeItem? Karaoke { get; set; }

        public virtual SingFollower? Follower { get; set; }
    }
}

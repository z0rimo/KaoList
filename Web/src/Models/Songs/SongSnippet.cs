// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Models.I18ns;
using CodeRabbits.KaoList.Web.Models.Thumbnails;

namespace CodeRabbits.KaoList.Web.Models.Songs
{
    public class SongSnippet
    {
        public virtual DateTime? Created { get; set; } = default!;

        public virtual string? Title { get; set; } = default!;

        public virtual IEnumerable<SongUser>? Songusers { get; set; } = default!;

        public virtual string? Composer { get; set; } = default!;

        public virtual ThumbnailResource? Thumbnail { get; set; }

        public virtual I18nLanguageResource? I18nName { get; set; }

        public virtual SongKaraokeItem? Karaoke { get; set; }
    }
}

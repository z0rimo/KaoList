// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Models.I18ns;
using CodeRabbits.KaoList.Web.Models.Thumbnails;

namespace CodeRabbits.KaoList.Web.Models.Songs
{
    public class SongSnippet
    {
        public DateTime? Created { get; set; } = default!;

        public string? Title { get; set; } = default!;

        public IEnumerable<SongUser>? SongUsers { get; set; } = default!;

        public ThumbnailResource? Thumbnail { get; set; }

        public I18nLanguageResource? I18nName { get; set; }

        public SongKaraokeItem? Karaoke { get; set; }
    }
}

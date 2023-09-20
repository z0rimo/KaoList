// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Models.Thumbnails;

namespace CodeRabbits.KaoList.Web.Models.MyPages
{
    public class MyPageProfileResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#myPageProfile";

        public string? Id { get; set; } = default!;

        public string? Email { get; set; }

        public string? Nickname { get; set; }

        public DateTime? NicknameEditedDateTime { get; set; }

        public MyPageExternalLogin? ExternalLogin { get; set; }

        public ThumbnailResource? Thumbnail { get; set; }
    }
}

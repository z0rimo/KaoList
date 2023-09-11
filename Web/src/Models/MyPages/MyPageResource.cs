// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.MyPages
{
    public class MyPageResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#myPage";

        public string? Email { get; set; }

        public string? Nickname { get; set; }

        public DateTime? NicknameEditedDateTime { get; set; }

        public List<MyPageSongSerachLog>? SongSearchQueryList { get; set; }

        public List<MyPageSignInLog>? SignInLogList { get; set; }

        public List<MyPageFollowedSong>? FollowedSongList { get; set; }

        public MyPageExternalLogin? ExternalLogin { get; set; }
    }
}

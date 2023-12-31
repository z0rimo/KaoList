// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.MyPages
{
    public class MyPageSongSearchLogResponse : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#myPageSongSearchLogResponse";

        public IEnumerable<MyPageSongSerachLogResource>? Resources { get; set; }
    }
}

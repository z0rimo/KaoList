// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.MyPages
{
    public class MyPageProfileResponse : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#myPageProfileResponse";

        public MyPageProfileResource? Item { get; set; }
    }
}

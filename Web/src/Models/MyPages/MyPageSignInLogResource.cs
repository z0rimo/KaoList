// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Net;
using System.Text.Json.Serialization;
using CodeRabbits.KaoList.Web.Utils;

namespace CodeRabbits.KaoList.Web.Models.MyPages
{
    public class MyPageSignInLogResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#signInLog";

        public int? Id { get; set; } = default!;

        public DateTime? Created { get; set; }

        [JsonConverter(typeof(IPAddressJsonConverter))]
        public IPAddress? IpAddress { get; set; }
    }
}

// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Net;
using System.Text.Json.Serialization;

namespace CodeRabbits.KaoList.Web.Models.MyPages
{
    public class MyPageSignInLog
    {
        public DateTime? Created { get; set; }

        [JsonConverter(typeof(IPAddressJsonConverter))]
        public IPAddress? IpAddress { get; set; }
    }
}

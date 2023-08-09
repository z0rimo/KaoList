// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace CodeRabbits.KaoList.Web.Models
{
    public abstract class KaoListResponse
    {
        public abstract string Kind { get; set; }

        [JsonPropertyName("etag")]
        public virtual string? Etag { get; set; }
    }
}

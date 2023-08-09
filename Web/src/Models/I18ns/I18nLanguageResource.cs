// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.I18ns
{
    public class I18nLanguageResource : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#i18nLanguage";

        public string? Id { get; set; }

        public I18nLanguageSnippet? Snippet { get; set; }
    }
}

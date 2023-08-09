// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Models.I18ns
{
    public class I18nLanguageListResponse : KaoListResponse
    {
        public override string Kind { get; set; } = "kaoList#i18nLanguageListResponse";

        public IEnumerable<I18nLanguageResource>? Items { get; set; }
    }
}

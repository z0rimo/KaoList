// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Utils
{
    public static class NormalizeTextHelper
    {
        private static readonly Dictionary<string, string> Synonyms = new()
        {
            { "feat.", "ft." },
            { "&", "and" }
        };

        private static readonly string[] TitleStopWords = new string[] { "드라마", "영화", "OST", "ost" };

        public static string NormalizeText(string text)
        {
            text = text.ToLower()
                       .Replace(" ", "")
                       .Replace("-", "")
                       .Replace("(", "")
                       .Replace(")", "")
                       .Replace("\"", "")
                       .Replace(".", "")
                       .Replace(",", "");

            foreach (var synonym in Synonyms)
            {
                text = text.Replace(synonym.Key, synonym.Value);
            }

            foreach (var stopWord in TitleStopWords)
            {
                text = text.Replace(stopWord, "");
            }

            return text;
        }
    }
}

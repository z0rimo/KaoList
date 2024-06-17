// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Text;

namespace CodeRabbits.KaoList.Web.Utils
{
    public static class SongTitleNormalizeHelper
    {
        private static readonly Dictionary<string, string> Synonyms = new()
        {
            { "feat.", "ft." },
            { "&", "and" }
        };

        private static readonly string[] TitleStopWords = new string[] { "드라마", "영화", "OST", "ost" };

        public static string NormalizeSongTitle(string text)
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

        public static string NormalizeQuery(string query)
        {
            var normalizedQuery = new StringBuilder();
            foreach (var c in query)
            {
                if (c >= '가' && c <= '힣')
                {
                    normalizedQuery.Append(c);
                }
                else
                {
                    normalizedQuery.Append(char.ToLowerInvariant(c));
                }
            }

            return normalizedQuery.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}

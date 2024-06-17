// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Utils
{
    public class CalculateSimilarityHelper
    {
        public double CalculateSimilarity(string sourceTitle, string sourceArtist, string? sourceComposer, string? sourceLyricist, string targetTitle, string targetArtist, string? targetComposer, string? targetLyricist)
        {
            var normalizedSourceTitleArtist = SongTitleNormalizeHelper.NormalizeSongTitle($"{sourceArtist} {sourceTitle}");
            var normalizedTargetTitleArtist = SongTitleNormalizeHelper.NormalizeSongTitle($"{targetArtist} {targetTitle}");

            var jaroWinkler = new JaroWinkler();
            double titleArtistSimilarity = jaroWinkler.Similarity(normalizedSourceTitleArtist, normalizedTargetTitleArtist);

            double composerSimilarity = CalculateSetMatchSimilarity(sourceComposer ?? "", targetComposer ?? "");
            double lyricistSimilarity = CalculateSetMatchSimilarity(sourceLyricist ?? "", targetLyricist ?? "");

            double totalSimilarity = (0.7 * titleArtistSimilarity) + (0.15 * composerSimilarity) + (0.15 * lyricistSimilarity);

            return totalSimilarity;
        }

        private double CalculateSetMatchSimilarity(string source, string target)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(target))
            {
                return 0.0;
            }

            var sourceParts = new HashSet<string>(source.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct());
            var targetParts = new HashSet<string>(target.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct());

            var matches = sourceParts.Intersect(targetParts).Count();
            var total = Math.Max(sourceParts.Count, targetParts.Count);

            return (double)matches / total;
        }

        public bool AreSongsSimilar(double similarityScore, double threshold = 0.85)
        {
            return similarityScore >= threshold;
        }
    }
}

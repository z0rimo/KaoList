// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Utils
{
    public class JaroWinkler
    {
        private const double DefaultThreshold = 0.7;
        private const int PrefixLength = 4;

        public double Similarity(string s1, string s2)
        {
            var mtp = Matches(s1, s2);
            var m = mtp[0];
            if (m == 0)
            {
                return 0.0;
            }
            double j = ((m / (double)s1.Length) + (m / (double)s2.Length) + ((m - mtp[1]) / (double)m)) / 3.0;
            double jw = j < DefaultThreshold ? j : j + Math.Min(DefaultThreshold, 1.0 / mtp[3]) * Math.Min(mtp[2], PrefixLength) * (1.0 - j);
            return jw;
        }

        private static int[] Matches(string s1, string s2)
        {
            string max, min;
            if (s1.Length > s2.Length)
            {
                max = s1;
                min = s2;
            }
            else
            {
                max = s2;
                min = s1;
            }
            var range = Math.Max(max.Length / 2 - 1, 0);
            var matchIndexes = new int[min.Length];
            for (var mi = 0; mi < matchIndexes.Length; mi++)
            {
                matchIndexes[mi] = -1;
            }
            var matchFlags = new bool[max.Length];
            var matches = 0;
            for (var mi = 0; mi < min.Length; mi++)
            {
                var c1 = min[mi];
                int xi = Math.Max(mi - range, 0);
                int xn = Math.Min(mi + range + 1, max.Length);
                for (; xi < xn; xi++)
                {
                    if (matchFlags[xi] || c1 != max[xi]) continue;
                    matchIndexes[mi] = xi;
                    matchFlags[xi] = true;
                    matches++;
                    break;
                }
            }
            var ms1 = new char[matches];
            var ms2 = new char[matches];
            for (int i = 0, si = 0; i < min.Length; i++)
            {
                if (matchIndexes[i] != -1)
                {
                    ms1[si] = min[i];
                    si++;
                }
            }
            for (int i = 0, si = 0; i < max.Length; i++)
            {
                if (matchFlags[i])
                {
                    ms2[si] = max[i];
                    si++;
                }
            }
            var transpositions = 0;
            for (var mi = 0; mi < ms1.Length; mi++)
            {
                if (ms1[mi] != ms2[mi])
                {
                    transpositions++;
                }
            }
            var prefix = 0;
            for (var mi = 0; mi < Math.Min(s1.Length, s2.Length); mi++)
            {
                if (s1[mi] == s2[mi])
                {
                    prefix++;
                }
                else
                {
                    break;
                }
            }
            return new[] { matches, transpositions / 2, prefix, max.Length };
        }
    }
}

// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Globalization;

namespace CodeRabbits.KaoList.Web;

internal static class SRHelper
{
    // The resource generator used in AspNetCore does not create this method. This file fills in that functional gap
    // so we don't have to modify the shared source.
    internal static string Format(string resourceFormat, params object[] args)
    {
        if (args != null)
        {
            return string.Format(CultureInfo.CurrentCulture, resourceFormat, args);
        }

        return resourceFormat;
    }
}

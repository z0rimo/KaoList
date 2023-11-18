// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Services.Mananas;

public class MananaSong
{
    public string Brand { get; set; } = default!;

    public string No { get; set; } = default!;

    public string Title { get; set; } = default!;

    public string Singer { get; set; } = default!;

    public string? Composer { get; set; }

    public string? Lyricist { get; set; }

    public DateTime? Release { get; set; }
}

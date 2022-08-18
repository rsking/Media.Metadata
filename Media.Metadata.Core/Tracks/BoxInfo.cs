// -----------------------------------------------------------------------
// <copyright file="BoxInfo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Tracks;

internal struct BoxInfo
{
    public long Offset { get; set; }

    public long BoxOffset { get; set; }

    public bool Last { get; set; }

    public AsciiType Type { get; set; }
}
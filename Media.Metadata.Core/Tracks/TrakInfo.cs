// -----------------------------------------------------------------------
// <copyright file="TrakInfo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Tracks;

internal struct TrakInfo
{
    public uint Id { get; set; }

    public string Type { get; set; }

    public long[] Samples { get; set; }

    public uint[] Durations { get; set; }

    public uint[] FrameCount { get; set; }

    public uint[] Chaps { get; set; }

    public uint TimeUnitPerSecond { get; set; }

    public string? Language { get; set; }
}
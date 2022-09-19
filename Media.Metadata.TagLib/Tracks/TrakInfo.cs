// -----------------------------------------------------------------------
// <copyright file="TrakInfo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Tracks;

/// <summary>
/// The TRAK info.
/// </summary>
[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
internal struct TrakInfo
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public uint Id { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the samples.
    /// </summary>
    public long[] Samples { get; set; }

    /// <summary>
    /// Gets or sets the durations.
    /// </summary>
    public uint[] Durations { get; set; }

    /// <summary>
    /// Gets or sets the frame count.
    /// </summary>
    public uint[] FrameCount { get; set; }

    /// <summary>
    /// Gets or sets the chapters.
    /// </summary>
    public uint[] Chaps { get; set; }

    /// <summary>
    /// Gets or sets the time unit per second.
    /// </summary>
    public uint TimeUnitPerSecond { get; set; }

    /// <summary>
    /// Gets or sets the duration.
    /// </summary>
    public ulong Duration { get; set; }

    /// <summary>
    /// Gets or sets the language.
    /// </summary>
    public string? Language { get; set; }
}
// -----------------------------------------------------------------------
// <copyright file="MoovInfo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Tracks;

/// <summary>
/// The MOOV info.
/// </summary>
[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
internal struct MoovInfo
{
    /// <summary>
    /// Gets or sets the time unit per second.
    /// </summary>
    public uint TimeUnitPerSecond { get; set; }

    /// <summary>
    /// Gets or sets the tracks.
    /// </summary>
    public TrakInfo[] Tracks { get; set; }
}
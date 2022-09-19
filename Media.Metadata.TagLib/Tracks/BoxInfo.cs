// -----------------------------------------------------------------------
// <copyright file="BoxInfo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Tracks;

/// <summary>
/// The BOX info.
/// </summary>
[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
internal struct BoxInfo
{
    /// <summary>
    /// Gets or sets the offset.
    /// </summary>
    public long Offset { get; set; }

    /// <summary>
    /// Gets or sets the box offset.
    /// </summary>
    public long BoxOffset { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is the last box.
    /// </summary>
    public bool Last { get; set; }

    /// <summary>
    /// Gets or sets the ASCII type.
    /// </summary>
    public AsciiType Type { get; set; }
}
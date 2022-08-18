// -----------------------------------------------------------------------
// <copyright file="MoovInfo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Tracks;

internal struct MoovInfo
{
    public uint TimeUnitPerSecond { get; set; }

    public TrakInfo[] Tracks { get; set; }
}
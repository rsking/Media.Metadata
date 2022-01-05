// -----------------------------------------------------------------------
// <copyright file="MediaTrackType.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

public enum MediaTrackType
{
    Unknown = -1,
    Video = -2,
    Audio = -3,
    All = int.MinValue,
}
﻿// -----------------------------------------------------------------------
// <copyright file="BoxInfo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Tracks;

/// <summary>
/// The BOX info.
/// </summary>
[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
internal readonly record struct BoxInfo(long Offset, long BoxOffset, bool Last, AsciiType Type);
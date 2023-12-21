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
internal readonly record struct TrakInfo(uint? Id, string? Type, IReadOnlyList<long>? Samples, IReadOnlyList<uint>? Durations, IReadOnlyList<uint>? FrameCount, IReadOnlyList<uint>? Chaps, uint? TimeUnitPerSecond, ulong? Duration, string? Language);
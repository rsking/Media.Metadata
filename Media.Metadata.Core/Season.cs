// -----------------------------------------------------------------------
// <copyright file="Season.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Represents a series.
/// </summary>
/// <param name="Number">Get the series number.</param>
/// <param name="Episodes">Gets the episodes.</param>
public record class Season(int Number, IEnumerable<Episode> Episodes) : HasImage;
// -----------------------------------------------------------------------
// <copyright file="Series.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Represents a show.
/// </summary>
/// <param name="Name">Gets the name.</param>
/// <param name="Seasons">Gets the seasons.</param>
public record Series(string Name, IEnumerable<Season> Seasons);
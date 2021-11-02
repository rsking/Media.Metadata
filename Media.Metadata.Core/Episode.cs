﻿// -----------------------------------------------------------------------
// <copyright file="Episode.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Represents an episode.
/// </summary>
public record Episode(
    string? Name,
    string? Description);
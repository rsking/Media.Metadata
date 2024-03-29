﻿// -----------------------------------------------------------------------
// <copyright file="ILocalVideo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// A local file.
/// </summary>
public interface ILocalVideo
{
    /// <summary>
    /// Gets the file info.
    /// </summary>
    FileInfo FileInfo { get; }
}
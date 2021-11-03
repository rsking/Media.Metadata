// -----------------------------------------------------------------------
// <copyright file="ILocalVideo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// A local file.
/// </summary>
internal interface ILocalVideo
{
    /// <summary>
    /// Gets the file info.
    /// </summary>
    System.IO.FileInfo FileInfo { get; }
}
// -----------------------------------------------------------------------
// <copyright file="LocalVideo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

internal record class LocalVideo(FileInfo FileInfo, string? Name) : Video(Name, default, default, default, default, default, default, default, default);
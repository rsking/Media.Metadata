// -----------------------------------------------------------------------
// <copyright file="MediaChapter.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

public record class MediaChapter(string Title, TimeSpan Duration);
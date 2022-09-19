// -----------------------------------------------------------------------
// <copyright file="MediaChapter.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The media chapter.
/// </summary>
/// <param name="Title">The title.</param>
/// <param name="Duration">The duration.</param>
public record class MediaChapter(string Title, TimeSpan Duration);
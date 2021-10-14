// -----------------------------------------------------------------------
// <copyright file="IUpdater.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

public interface IUpdater
{
    void UpdateMovie(string fileName, Movie movie);

    void UpdateEpisode(string fileName, Episode episode);
}
// -----------------------------------------------------------------------
// <copyright file="IAbstractStream.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Tracks;

/// <summary>
/// The abstract stream.
/// </summary>
internal interface IAbstractStream
{
    /// <summary>
    /// Gets the length.
    /// </summary>
    long Length { get; }

    /// <summary>
    /// Gets the position.
    /// </summary>
    long Position { get; }

    /// <inheritdoc cref="Stream.Seek(long, SeekOrigin)"/>
    void Seek(SeekOrigin origin, long offset);

    /// <inheritdoc cref="Stream.Read(byte[], int, int)"/>
    int Read(byte[] buffer, int bytes);
}
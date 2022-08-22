// -----------------------------------------------------------------------
// <copyright file="StreamWrapper.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Tracks;

/// <summary>
/// A <see cref="Stream"/> <see cref="IAbstractStream"/>.
/// </summary>
internal sealed class StreamWrapper : IAbstractStream
{
    private readonly Stream stream;

    /// <summary>
    /// Initialises a new instance of the <see cref="StreamWrapper"/> class.
    /// </summary>
    /// <param name="stream">The stream.</param>
    public StreamWrapper(Stream stream) => this.stream = stream;

    /// <inheritdoc/>
    public long Length => this.stream.Length;

    /// <inheritdoc/>
    public long Position => this.stream.Position;

    /// <inheritdoc/>
    public void Seek(SeekOrigin origin, long offset) => this.stream.Seek(offset, origin);

    /// <inheritdoc/>
    public int Read(byte[] buffer, int bytes) => this.stream.Read(buffer, 0, bytes);
}

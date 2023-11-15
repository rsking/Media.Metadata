// -----------------------------------------------------------------------
// <copyright file="AsciiType.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Tracks;

/// <summary>
/// The ASCII type.
/// </summary>
internal struct AsciiType : IEquatable<byte[]>
{
    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public byte[] Type { get; set; }

    public static bool operator ==(AsciiType lhs, byte[] rhs) => lhs.Equals(rhs);

    public static bool operator !=(AsciiType lhs, byte[] rhs) => !lhs.Equals(rhs);

    /// <inheritdoc/>
    public readonly bool Equals(byte[] other) => this.Type[0] == other[0]
        && this.Type[1] == other[1]
        && this.Type[2] == other[2]
        && this.Type[3] == other[3];

    /// <inheritdoc/>
    public override readonly bool Equals(object obj) => obj is byte[] bytes && this.Equals(bytes);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => this.Type[0].GetHashCode() ^ this.Type[1].GetHashCode() ^ this.Type[2].GetHashCode() ^ this.Type[3].GetHashCode();

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        var c = new char[4];
        _ = System.Text.Encoding.ASCII.GetDecoder().GetChars(this.Type, 0, 4, c, 0);
        return new string(c);
    }
}
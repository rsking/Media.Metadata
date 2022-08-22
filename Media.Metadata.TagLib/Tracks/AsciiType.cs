// -----------------------------------------------------------------------
// <copyright file="AsciiType.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Tracks;

/// <summary>
/// The ASCII type.
/// </summary>
internal struct AsciiType
{
    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public byte[] Type { get; set; }

    /// <summary>
    /// Checks the type.
    /// </summary>
    /// <param name="refType">The ref type.</param>
    /// <returns><see langword="true"/> if <paramref name="refType"/> equals <see cref="Type"/>; otherwise <see langword="false"/>.</returns>
    public readonly bool Check(byte[] refType) => this.Type[0] == refType[0]
        && this.Type[1] == refType[1]
        && this.Type[2] == refType[2]
        && this.Type[3] == refType[3];

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        var c = new char[4];
        _ = System.Text.Encoding.ASCII.GetDecoder().GetChars(this.Type, 0, 4, c, 0);
        return new string(c);
    }
}
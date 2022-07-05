// -----------------------------------------------------------------------
// <copyright file="Mp4File.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// mp4v2 file.
/// </summary>
internal sealed class Mp4File
{
    private readonly string fileName;

    /// <summary>
    /// Initialises a new instance of the <see cref="Mp4File"/> class.
    /// </summary>
    /// <param name="fileName">The full path and file name of the file to use.</param>
    private Mp4File(string fileName)
    {
        if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
        {
            throw new ArgumentException("Must specify a valid file name", nameof(fileName));
        }

        this.fileName = fileName;
    }

    /// <summary>
    /// Gets the list of chapters for this file.
    /// </summary>
    public ChapterList? Chapters { get; private set; }

    /// <summary>
    /// Gets the metadata tags for this file.
    /// </summary>
    public MetadataTags? Tags { get; private set; }

    /// <summary>
    /// Gets the tracks for this file.
    /// </summary>
    public TrackList? Tracks { get; private set; }

    /// <summary>
    /// Opens and reads the data for the specified file.
    /// </summary>
    /// <param name="fileName">The full path and file name of the MP4 file to open.</param>
    /// <returns>An <see cref="Mp4File"/> object you can use to manipulate file.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the specified file name is <see langword="null"/> or the empty string.
    /// </exception>
    public static Mp4File Open(string fileName)
    {
        var file = new Mp4File(fileName);
        file.Load();
        return file;
    }

    /// <summary>
    /// Loads the metadata for this file.
    /// </summary>
    public void Load()
    {
        var fileHandle = NativeMethods.MP4Read(System.Text.Encoding.UTF8.GetBytes(this.fileName), IntPtr.Zero);
        if (fileHandle == IntPtr.Zero)
        {
            // open for modify to see if that enables us to read
            fileHandle = NativeMethods.MP4Modify(System.Text.Encoding.UTF8.GetBytes(this.fileName), 0);
        }

        if (fileHandle != IntPtr.Zero)
        {
            try
            {
                this.Tags = MetadataTags.ReadFromFile(fileHandle);
                this.Chapters = ChapterList.ReadFromFile(fileHandle);
                this.Tracks = TrackList.ReadFromFile(fileHandle);
            }
            finally
            {
                NativeMethods.MP4Close(fileHandle);
            }
        }
    }

    /// <summary>
    /// Saves the edits, if any, to the metadata for this file.
    /// </summary>
    public void Save()
    {
        var fileHandle = NativeMethods.MP4Modify(System.Text.Encoding.UTF8.GetBytes(this.fileName), 0);
        if (fileHandle != IntPtr.Zero)
        {
            try
            {
                this.Tags?.WriteToFile(fileHandle);
                this.Chapters?.WriteToFile(fileHandle);
                this.Tracks?.WriteToFile(fileHandle);
            }
            finally
            {
                NativeMethods.MP4Close(fileHandle);
            }
        }
    }
}
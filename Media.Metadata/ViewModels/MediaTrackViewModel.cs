// -----------------------------------------------------------------------
// <copyright file="MediaTrackViewModel.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.ViewModels;

using System.Text;

/// <summary>
/// The <see cref="MediaTrack"/> view model.
/// </summary>
internal partial class MediaTrackViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    private static IEnumerable<string>? languages;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private string? selectedLanguage;

    /// <summary>
    /// Initialises a new instance of the <see cref="MediaTrackViewModel"/> class.
    /// </summary>
    /// <param name="mediaTrack">The media track.</param>
    public MediaTrackViewModel(MediaTrack mediaTrack)
    {
        this.Id = mediaTrack.Id;
        this.Type = mediaTrack.Type;
        this.selectedLanguage = mediaTrack.Language;
    }

    /// <summary>
    /// Gets the ID.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets the type.
    /// </summary>
    public MediaTrackType Type { get; init; }

    /// <summary>
    /// Gets the languages.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "For data binding")]
    public IEnumerable<string> Languages => GetLanguages();

    /// <summary>
    /// Converts this to a video.
    /// </summary>
    /// <returns>The video.</returns>
    public MediaTrack ToMediaTrack() => new(this.Id, this.Type, this.selectedLanguage);

    private static IEnumerable<string> GetLanguages()
    {
        return languages ??= ReadLanguages().ToList();

        static IEnumerable<string> ReadLanguages()
        {
            var stream = typeof(App).Assembly.GetManifestResourceStream(typeof(App), "ISO-639-2_utf-8.txt") ?? throw new System.InvalidOperationException();
            using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: false);

            while (reader.ReadLine() is string line)
            {
                var split = line.Split('|');
                yield return split[0];
            }
        }
    }
}
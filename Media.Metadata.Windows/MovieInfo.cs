﻿// -----------------------------------------------------------------------
// <copyright file="MovieInfo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Windows;

using System.Text;

/// <summary>
/// The <see cref="MovieInfo"/> class is represents all of the information contained
/// in the "iTunMOVI" atom. This information includes such items as the cast, directors,
/// producers, and writers.
/// </summary>
internal class MovieInfo : Atom
{
    private IList<string>? cast;
    private IList<string>? directors;
    private IList<string>? producers;
    private IList<string>? screenwriters;
#pragma warning disable IDE0032
    private string? studio;
#pragma warning restore IDE0032

    /// <summary>
    /// Gets or sets the studio responsible for releasing this movie.
    /// </summary>
#pragma warning disable IDE0032, RCS1085, S2292
    public string? Studio { get => this.studio; set => this.studio = value; }
#pragma warning restore IDE0032, RCS1085, S2292

    /// <summary>
    /// Gets a list of cast members for this movie.
    /// </summary>
    /// <remarks>
    /// The <see cref="Cast"/> property is read-only, but can be modified by the normal methods of a <see cref="IList{T}"/>.
    /// There is a distinction to be drawn between an empty list where the cast portion of the atom exists in the file, but with no entries, and the state where the cast portion does not exist at all in the file.
    /// To handle the latter case, use the <see cref="RemoveCast"/> method.
    /// Note that if the cast portion of the atom does not exist, accessing the <see cref="Cast"/> property will create an empty list, adding an empty list to that portion of the atom.
    /// </remarks>
    public IList<string> Cast => this.cast ??= new List<string>();

    /// <summary>
    /// Gets a list of directors for this movie.
    /// </summary>
    /// <remarks>
    /// The <see cref="Directors"/> property is read-only, but can be modified by the normal methods of a <see cref="IList{T}"/>.
    /// There is a distinction to be drawn between an empty list where the cast portion of the atom exists in the file, but with no entries, and the state where the directors portion does not exist at all in the file.
    /// To handle the latter case, use the <see cref="RemoveDirectors"/> method.
    /// Note that if the directors portion of the atom does not exist, accessing the <see cref="Directors"/> property will create an empty list, adding an empty list to that portion of the atom.
    /// </remarks>
    public IList<string> Directors => this.directors ??= new List<string>();

    /// <summary>
    /// Gets a list of producers for this movie.
    /// </summary>
    /// <remarks>
    /// The <see cref="Producers"/> property is read-only, but can be modified by the normal methods of a <see cref="IList{T}"/>.
    /// There is a distinction to be drawn between an empty list where the cast portion of the atom exists in the file, but with no entries, and the state where the producers portion does not exist at all in the file.
    /// To handle the latter case, use the <see cref="RemoveProducers"/> method.
    /// Note that if the producers portion of the atom does not exist, accessing the <see cref="Producers"/> property will create an empty list, adding an empty list to that portion of the atom.
    /// </remarks>
    public IList<string> Producers => this.producers ??= new List<string>();

    /// <summary>
    /// Gets a list of screenwriters for this movie.
    /// </summary>
    /// <remarks>
    /// The <see cref="Screenwriters"/> property is read-only, but can be modified by the normal methods of a <see cref="IList{T}"/>.
    /// There is a distinction to be drawn between an empty list where the cast portion of the atom exists in the file, but with noentries, and the state where the writers portion does not existat all in the file.
    /// To handle the latter case, use the <see cref="RemoveScreenwriters"/> method.
    /// Note that if the writers portion of the atom does not exist, accessing the <see cref="Screenwriters"/> property will create an empty list, adding an empty list to that portion of the atom.
    /// </remarks>
    public IList<string> Screenwriters => this.screenwriters ??= new List<string>();

    /// <summary>
    /// Gets a value indicating whether the <see cref="Cast"/> property has data, potentially including an empty list.
    /// Returns <see langword="false"/> if the <see cref="Cast"/> property is <see langword="null"/>.
    /// </summary>
    public bool HasCast => this.cast is not null;

    /// <summary>
    /// Gets a value indicating whether the <see cref="Directors"/> property has data, potentially including an empty list.
    /// Returns <see langword="false"/> if the <see cref="Directors"/> property is <see langword="null"/>.
    /// </summary>
    public bool HasDirectors => this.directors is not null;

    /// <summary>
    /// Gets a value indicating whether the <see cref="Producers"/> property has data, potentially including an empty list.
    /// Returns <see langword="false"/> if the <see cref="Producers"/> property is <see langword="null"/>.
    /// </summary>
    public bool HasProducers => this.producers is not null;

    /// <summary>
    /// Gets a value indicating whether the <see cref="Screenwriters"/> property has data, potentially including an empty list.
    /// Returns <see langword="false"/> if the <see cref="Screenwriters"/> property is <see langword="null"/>.
    /// </summary>
    public bool HasScreenwriters => this.screenwriters is not null;

    /// <summary>
    /// Gets the meaning of the atom.
    /// </summary>
    public override string Meaning => "com.apple.iTunes";

    /// <summary>
    /// Gets the name of the atom.
    /// </summary>
    public override string Name => "iTunMOVI";

    /// <summary>
    /// Removes all data from the <see cref="Cast"/> property, causing it to be <see langword="null"/>.
    /// </summary>
    public void RemoveCast() => this.cast = null;

    /// <summary>
    /// Removes all data from the <see cref="Directors"/> property, causing it to be <see langword="null"/>.
    /// </summary>
    public void RemoveDirectors() => this.directors = null;

    /// <summary>
    /// Removes all data from the <see cref="Producers"/> property, causing it to be <see langword="null"/>.
    /// </summary>
    public void RemoveProducers() => this.producers = null;

    /// <summary>
    /// Removes all data from the <see cref="Screenwriters"/> property, causing it to be <see langword="null"/>.
    /// </summary>
    public void RemoveScreenwriters() => this.screenwriters = null;

    /// <summary>
    /// Populates this <see cref="MovieInfo"/> with the specific data stored in it.
    /// </summary>
    /// <param name="dataBuffer">A byte array containing the iTunes Metadata Format data
    /// used to populate this <see cref="MovieInfo"/>.</param>
    public override void Populate(byte[] dataBuffer)
    {
        PList map;
        using (var stream = new MemoryStream(dataBuffer))
        {
            map = PList.Create(stream);
        }

        if (map is not null)
        {
            this.cast = GetListOrDefault<string>(map, nameof(this.cast));
            this.directors = GetListOrDefault<string>(map, nameof(this.directors));
            this.producers = GetListOrDefault<string>(map, nameof(this.producers));
            this.screenwriters = GetListOrDefault<string>(map, nameof(this.screenwriters));
            this.studio = GetOrDefault<string>(map, nameof(this.studio));

            static T? GetOrDefault<T>(PList plist, string key)
            {
                return plist.ContainsKey(key) ? (T)plist[key] : default;
            }

            static IList<T>? GetListOrDefault<T>(PList plist, string key)
            {
                return plist.ContainsKey(key)
                    ? Enumerate((object[])plist[key]).ToList()
                    : default;

                static IEnumerable<T> Enumerate(object[] values)
                {
                    return values.OfType<IDictionary<string, object>>().SelectMany(dictionary => dictionary.Values.OfType<T>());
                }
            }
        }
    }

    /// <summary>
    /// Returns the data to be stored in this <see cref="MovieInfo"/> as a byte array.
    /// </summary>
    /// <returns>The byte array containing the data to be stored in the atom.</returns>
    public override byte[] ToByteArray()
    {
        var map = new PList();
        map.AddIfNotNull(nameof(this.cast), this.cast);
        map.AddIfNotNull(nameof(this.directors), this.directors);
        map.AddIfNotNull(nameof(this.producers), this.producers);
        map.AddIfNotNull(nameof(this.screenwriters), this.screenwriters);
        map.AddIfNotNullOrEmpty(nameof(this.studio), this.studio);

        return Encoding.UTF8.GetBytes(map.ToString());
    }
}
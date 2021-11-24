// -----------------------------------------------------------------------
// <copyright file="EditableRating.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Models;

/// <summary>
/// An editable rating.
/// </summary>
internal partial class EditableRating : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    private readonly System.Collections.ObjectModel.ObservableCollection<Rating> ratings = new();

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private Country? selectedCountry;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private Rating? selectedRating;

    /// <summary>
    /// Initialises a new instance of the <see cref="EditableRating"/> class.
    /// </summary>
    /// <param name="rating">The rating.</param>
    public EditableRating(Rating? rating)
    {
        this.Countries = Country.All;
        this.SelectedCountry = Rating.GetCountry(rating);
        this.SelectedRating = rating;
    }

    /// <summary>
    /// Gets the countries.
    /// </summary>
    public IEnumerable<Country> Countries { get; }

    /// <summary>
    /// Gets the ratings.
    /// </summary>
    public IEnumerable<Rating> Ratings => this.ratings;

    /// <inheritdoc/>
    protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (string.Equals(e.PropertyName, nameof(this.selectedCountry), System.StringComparison.OrdinalIgnoreCase))
        {
            this.ratings.Clear();
            if (this.selectedCountry is Country country)
            {
                foreach (var rating in Rating.GetRatings(country))
                {
                    this.ratings.Add(rating);
                }
            }
        }

        base.OnPropertyChanged(e);
    }
}
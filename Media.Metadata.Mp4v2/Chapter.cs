// -----------------------------------------------------------------------
// <copyright file="Chapter.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Represents a chapter in an MP4 file.
/// </summary>
internal class Chapter
{
    private Guid id = Guid.NewGuid();
    private string title = string.Empty;
    private TimeSpan duration = TimeSpan.FromSeconds(0);

    /// <summary>
    /// Occurs when the value of any property is changed.
    /// </summary>
    internal event EventHandler? Changed;

    /// <summary>
    /// Gets or sets the title of this chapter.
    /// </summary>
    public string Title
    {
        get => this.title;
        set
        {
            if (!string.Equals(this.title, value, StringComparison.Ordinal))
            {
                this.title = value;
                this.OnChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets or sets the duration of this chapter.
    /// </summary>
    public TimeSpan Duration
    {
        get => this.duration;
        set
        {
            if (this.duration != value)
            {
                this.duration = value;
                this.OnChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets the internal ID of this Chapter.
    /// </summary>
    internal Guid Id => this.id;

    /// <summary>
    /// Returns the string representation of this chapter.
    /// </summary>
    /// <returns>The string representation of the chapter.</returns>
    public override string ToString() => FormattableString.Invariant($"{this.Title} ({this.Duration.TotalMilliseconds} milliseconds)");

    /// <summary>
    /// Returns the hash code for this <see cref="Chapter"/>.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(this.ToString());

    /// <summary>
    /// Determines whether two <see cref="Chapter"/> objects have the same value.
    /// </summary>
    /// <param name="obj">Determines whether this instance and a specified object, which
    /// must also be a <see cref="Chapter"/> object, have the same value.</param>
    /// <returns><see langword="true"/> if the object is a <see cref="Chapter"/> and its value
    /// is the same as this instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object obj) => obj is Chapter other && string.Equals(this.Title, other.Title, StringComparison.Ordinal) && this.Duration == other.Duration;

    /// <summary>
    /// Raises the <see cref="Changed"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected void OnChanged(EventArgs e) => this.Changed?.Invoke(this, e);
}
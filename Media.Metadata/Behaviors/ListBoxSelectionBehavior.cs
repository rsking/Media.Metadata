// -----------------------------------------------------------------------
// <copyright file="ListBoxSelectionBehavior.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Behaviors;

/// <summary>
/// <see cref="ListBox"/> selection behavior.
/// </summary>
/// <typeparam name="T">The type of item.</typeparam>
internal class ListBoxSelectionBehavior<T> : Microsoft.Xaml.Interactivity.Behavior<Microsoft.UI.Xaml.Controls.ListBox>
{
    /// <summary>
    /// Identifies the <see cref="SelectedItems"/> dependency property.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "RCS1158:Static member in generic type should use a type parameter.", Justification = "Checked")]
    public static readonly Microsoft.UI.Xaml.DependencyProperty SelectedItemsProperty =
        Microsoft.UI.Xaml.DependencyProperty.Register(
            nameof(SelectedItems),
            typeof(System.Collections.IList),
            typeof(ListBoxSelectionBehavior<T>),
            new Microsoft.UI.Xaml.PropertyMetadata(default, OnSelectedItemsChanged));

    private bool viewHandled;

    private bool modelHandled;

    /// <summary>
    /// Gets or sets the selected items.
    /// </summary>
    public System.Collections.IList SelectedItems
    {
        get => (System.Collections.IList)this.GetValue(SelectedItemsProperty);
        set => this.SetValue(SelectedItemsProperty, value);
    }

    /// <inheritdoc/>
    protected override void OnAttached()
    {
        base.OnAttached();

        this.AssociatedObject.SelectionChanged += this.OnListBoxSelectionChanged;
    }

    /// <inheritdoc />
    protected override void OnDetaching()
    {
        base.OnDetaching();

        if (this.AssociatedObject is { } associatedObject)
        {
            associatedObject.SelectionChanged -= this.OnListBoxSelectionChanged;
        }
    }

    private static void OnSelectedItemsChanged(Microsoft.UI.Xaml.DependencyObject sender, Microsoft.UI.Xaml.DependencyPropertyChangedEventArgs args)
    {
        if (sender is ListBoxSelectionBehavior<T> { modelHandled: false, AssociatedObject: not null } behavior)
        {
            behavior.modelHandled = true;
            behavior.SelectItems();
            behavior.modelHandled = false;
        }
    }

    // Propagate selected items from model to view
    private void SelectItems()
    {
        this.viewHandled = true;
        this.AssociatedObject.SelectedItems.Clear();
        if (this.SelectedItems is { } selectedItems)
        {
            foreach (var item in selectedItems)
            {
                this.AssociatedObject.SelectedItems.Add(item);
            }
        }

        this.viewHandled = false;
    }

    // Propagate selected items from view to model
    private void OnListBoxSelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs args)
    {
        if (this.viewHandled || this.AssociatedObject.ItemsSource is null)
        {
            return;
        }

        this.SelectedItems.Clear();
        if (this.AssociatedObject.SelectedItems is { } selectedItems)
        {
            foreach (var item in selectedItems)
            {
                _ = this.SelectedItems.Add(item);
            }
        }
    }
}
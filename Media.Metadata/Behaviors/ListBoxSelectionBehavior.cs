// -----------------------------------------------------------------------
// <copyright file="ListBoxSelectionBehavior.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Behaviors;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

/// <summary>
/// <see cref="ListBox"/> selection behavior.
/// </summary>
/// <typeparam name="T">The type of item.</typeparam>
internal class ListBoxSelectionBehavior<T> : Microsoft.Xaml.Interactivity.Behavior<ListBox>
{
    /// <summary>
    /// Identifies the <see cref="SelectedItems"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.Register(
            nameof(SelectedItems),
            typeof(System.Collections.IList),
            typeof(ListBoxSelectionBehavior<T>),
            new PropertyMetadata(default, OnSelectedItemsChanged));

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

        if (this.AssociatedObject is not null)
        {
            this.AssociatedObject.SelectionChanged -= this.OnListBoxSelectionChanged;
        }
    }

    private static void OnSelectedItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        var behavior = (ListBoxSelectionBehavior<T>)sender;
        if (behavior.modelHandled)
        {
            return;
        }

        if (behavior.AssociatedObject is null)
        {
            return;
        }

        behavior.modelHandled = true;
        behavior.SelectItems();
        behavior.modelHandled = false;
    }

    // Propagate selected items from model to view
    private void SelectItems()
    {
        this.viewHandled = true;
        this.AssociatedObject.SelectedItems.Clear();
        if (this.SelectedItems is not null)
        {
            foreach (var item in this.SelectedItems)
            {
                this.AssociatedObject.SelectedItems.Add(item);
            }
        }

        this.viewHandled = false;
    }

    // Propagate selected items from view to model
    private void OnListBoxSelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        if (this.viewHandled)
        {
            return;
        }

        if (this.AssociatedObject.ItemsSource is null)
        {
            return;
        }

        this.SelectedItems.Clear();
        if (this.AssociatedObject.SelectedItems is not null)
        {
            foreach (var item in this.AssociatedObject.SelectedItems)
            {
                this.SelectedItems.Add(item);
            }
        }
    }
}
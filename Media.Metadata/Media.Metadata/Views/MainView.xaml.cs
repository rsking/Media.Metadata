// -----------------------------------------------------------------------
// <copyright file="MainView.xaml.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Views;

using Microsoft.UI.Xaml.Controls;

/// <summary>
/// Interaction logic for <c>MainView.xaml</c>.
/// </summary>
public sealed partial class MainView : UserControl
{
    /// <summary>
    /// Initialises a new instance of the <see cref="MainView"/> class.
    /// </summary>
    public MainView()
    {
        this.InitializeComponent();
        this.DataContext = Microsoft.Toolkit.Mvvm.DependencyInjection.Ioc.Default.GetRequiredService<ViewModels.MainViewModel>();
    }
}
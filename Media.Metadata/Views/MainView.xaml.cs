// -----------------------------------------------------------------------
// <copyright file="MainView.xaml.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Views;

/// <summary>
/// Interaction logic for <c>MainView.xaml</c>.
/// </summary>
public sealed partial class MainView : Microsoft.UI.Xaml.Controls.UserControl
{
    /// <summary>
    /// Initialises a new instance of the <see cref="MainView"/> class.
    /// </summary>
    public MainView()
    {
        this.InitializeComponent();
        this.DataContext = CommunityToolkit.Mvvm.DependencyInjection.Ioc.Default.GetRequiredService<ViewModels.MainViewModel>();
        this.UpdateMapDetails();
    }

    /// <inheritdoc/>
    protected override void OnApplyTemplate() => this.UpdateMapDetails();

    private void UpdateMapDetails()
    {
        if (this.ListDetailsView is CommunityToolkit.WinUI.UI.Controls.ListDetailsView { MapDetails: null } listDetailsView)
        {
            listDetailsView.MapDetails = value =>
            {
                if (this.DataContext is ViewModels.MainViewModel viewModel)
                {
                    viewModel.SetEditableVideo(value as Video);
                    return viewModel.SelectedEditableVideo;
                }

                return default;
            };
        }
    }
}
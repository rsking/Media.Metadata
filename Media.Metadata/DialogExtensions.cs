// -----------------------------------------------------------------------
// <copyright file="DialogExtensions.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Dialog extensions.
/// </summary>
internal static class DialogExtensions
{
    /// <summary>
    /// Initialises the file open picker.
    /// </summary>
    /// <param name="picker">The picker to initialise.</param>
    /// <param name="window">The window.</param>
    public static void Init(this Windows.Storage.Pickers.FileOpenPicker picker, Microsoft.UI.Xaml.Window? window = default) => InitPicker(picker, window);

    /// <summary>
    /// Initialises the file save picker.
    /// </summary>
    /// <param name="picker">The picker to initialise.</param>
    /// <param name="window">The window.</param>
    public static void Init(this Windows.Storage.Pickers.FileSavePicker picker, Microsoft.UI.Xaml.Window? window = default) => InitPicker(picker, window);

    /// <summary>
    /// Initialises the folder picker.
    /// </summary>
    /// <param name="picker">The picker to initialise.</param>
    /// <param name="window">The window.</param>
    public static void Init(this Windows.Storage.Pickers.FolderPicker picker, Microsoft.UI.Xaml.Window? window = default) => InitPicker(picker, window);

    private static void InitPicker(object picker, Microsoft.UI.Xaml.Window? window)
    {
        if (Microsoft.UI.Xaml.Window.Current is null)
        {
            var hwnd = window is null
                ? (System.IntPtr)Vanara.PInvoke.User32.GetActiveWindow()
                : WinRT.Interop.WindowNative.GetWindowHandle(window);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
        }
    }
}
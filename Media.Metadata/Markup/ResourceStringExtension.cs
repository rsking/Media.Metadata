namespace Media.Metadata.Markup;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

//using Microsoft.Windows.ApplicationModel.Resources;

[Microsoft.UI.Xaml.Markup.MarkupExtensionReturnType(ReturnType = typeof(string))]
public sealed class ResourceString : Microsoft.UI.Xaml.Markup.MarkupExtension
{
    //private static ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView();

    public string? Name { get; set; }

    protected override object? ProvideValue()
    {
        return this.Name is string name ? UI.Properties.Resources.ResourceManager.GetString(name, UI.Properties.Resources.Culture) : default;
    }
}
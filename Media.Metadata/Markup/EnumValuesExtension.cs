// -----------------------------------------------------------------------
// <copyright file="EnumValuesExtension.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Markup;

/// <summary>
/// The <see langword="enum"/> type extension.
/// </summary>
[Microsoft.UI.Xaml.Markup.MarkupExtensionReturnType(ReturnType = typeof(System.Array))]
public sealed class EnumValuesExtension : Microsoft.UI.Xaml.Markup.MarkupExtension
{
    /// <summary>
    /// Gets or sets the <see cref="Type"/> of the target <see langword="enum"/>.
    /// </summary>
    public System.Type? EnumType { get; set; }

    /// <inheritdoc/>
    protected override object ProvideValue() => this.EnumType switch
    {
        not null => System.Enum.GetValues(this.EnumType),
        _ => System.Array.Empty<object>(),
    };
}
// -----------------------------------------------------------------------
// <copyright file="HostBinder.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// The <see cref="Microsoft.Extensions.Hosting.IHost"/> <see cref="System.CommandLine.Binding.BinderBase{T}"/>.
/// </summary>
internal class HostBinder : System.CommandLine.Binding.BinderBase<Microsoft.Extensions.Hosting.IHost>
{
    /// <summary>
    /// The instance.
    /// </summary>
    public static readonly System.CommandLine.Binding.IValueDescriptor<Microsoft.Extensions.Hosting.IHost> Instance = new HostBinder();

    /// <inheritdoc/>
    protected override Microsoft.Extensions.Hosting.IHost GetBoundValue(System.CommandLine.Binding.BindingContext bindingContext) => bindingContext.GetRequiredService<Microsoft.Extensions.Hosting.IHost>();
}
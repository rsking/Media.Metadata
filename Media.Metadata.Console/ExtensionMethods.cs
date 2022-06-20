// -----------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Binding;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class ExtensionMethods
{
    public static void SetHandler<T>(
        this Command command,
        Func<T, CancellationToken, Task> handle,
        IValueDescriptor<T> symbol)
    {
        command.SetHandler(context =>
        {
            var value = context.GetValueForHandlerParameter(symbol)!;
            return handle(value, context.GetCancellationToken());
        });
    }

    public static void SetHandler<T1, T2>(
        this Command command,
        Func<T1, T2, CancellationToken, Task> handle,
        IValueDescriptor<T1> symbol1,
        IValueDescriptor<T2> symbol2)
    {
        command.SetHandler(context =>
        {
            var value1 = context.GetValueForHandlerParameter(symbol1)!;
            var value2 = context.GetValueForHandlerParameter(symbol2)!;
            return handle(value1, value2, context.GetCancellationToken());
        });
    }

    public static void SetHandler<T1, T2, T3>(
        this Command command,
        Func<T1, T2, T3, CancellationToken, Task> handle,
        IValueDescriptor<T1> symbol1,
        IValueDescriptor<T2> symbol2,
        IValueDescriptor<T3> symbol3)
    {
        command.SetHandler(context =>
        {
            var value1 = context.GetValueForHandlerParameter(symbol1)!;
            var value2 = context.GetValueForHandlerParameter(symbol2)!;
            var value3 = context.GetValueForHandlerParameter(symbol3)!;
            return handle(value1, value2, value3, context.GetCancellationToken());
        });
    }

    public static void SetHandler<T1, T2, T3, T4, T5>(
        this Command command,
        Func<IConsole, T1, T2, T3, T4, T5, CancellationToken, Task> handle,
        IValueDescriptor<T1> symbol1,
        IValueDescriptor<T2> symbol2,
        IValueDescriptor<T3> symbol3,
        IValueDescriptor<T4> symbol4,
        IValueDescriptor<T5> symbol5)
    {
        command.SetHandler(context =>
        {
            var value1 = context.GetValueForHandlerParameter(symbol1)!;
            var value2 = context.GetValueForHandlerParameter(symbol2)!;
            var value3 = context.GetValueForHandlerParameter(symbol3)!;
            var value4 = context.GetValueForHandlerParameter(symbol4)!;
            var value5 = context.GetValueForHandlerParameter(symbol5)!;
            return handle(context.Console, value1, value2, value3, value4, value5, context.GetCancellationToken());
        });
    }

    public static void SetHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        this Command command,
        Func<IConsole, T1, T2, T3, T4, T5, T6, T7, T8, T9, CancellationToken, Task> handle,
        IValueDescriptor<T1> symbol1,
        IValueDescriptor<T2> symbol2,
        IValueDescriptor<T3> symbol3,
        IValueDescriptor<T4> symbol4,
        IValueDescriptor<T5> symbol5,
        IValueDescriptor<T6> symbol6,
        IValueDescriptor<T7> symbol7,
        IValueDescriptor<T8> symbol8,
        IValueDescriptor<T9> symbol9)
    {
        command.SetHandler(context =>
        {
            var value1 = context.GetValueForHandlerParameter(symbol1)!;
            var value2 = context.GetValueForHandlerParameter(symbol2)!;
            var value3 = context.GetValueForHandlerParameter(symbol3)!;
            var value4 = context.GetValueForHandlerParameter(symbol4)!;
            var value5 = context.GetValueForHandlerParameter(symbol5)!;
            var value6 = context.GetValueForHandlerParameter(symbol6)!;
            var value7 = context.GetValueForHandlerParameter(symbol7)!;
            var value8 = context.GetValueForHandlerParameter(symbol8)!;
            var value9 = context.GetValueForHandlerParameter(symbol9)!;
            return handle(context.Console, value1, value2, value3, value4, value5, value6, value7, value8, value9, context.GetCancellationToken());
        });
    }

    public static T? GetValueForHandlerParameter<T>(this System.CommandLine.Invocation.InvocationContext context, System.CommandLine.Binding.IValueDescriptor<T> symbol) =>
        symbol is IValueSource valueSource && valueSource.TryGetValue(symbol, context.BindingContext, out var boundValue) && boundValue is T value
            ? value
            : context.ParseResult.GetValueFor(symbol);

    private static T? GetValueFor<T>(this System.CommandLine.Parsing.ParseResult parseResult, System.CommandLine.Binding.IValueDescriptor<T> symbol) => symbol switch
    {
        Argument<T> argument => parseResult.GetValueForArgument(argument),
        Option<T> option => parseResult.GetValueForOption(option),
        _ => throw new ArgumentOutOfRangeException(nameof(symbol)),
    };
}

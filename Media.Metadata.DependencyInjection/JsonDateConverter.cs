// -----------------------------------------------------------------------
// <copyright file="JsonDateConverter.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System.Buffers;

/// <summary>
/// A <see cref="System.Text.Json.Serialization.JsonConverter{T}"/> for <see cref="Nullable{DateTime}"/>.
/// </summary>
internal sealed class JsonDateConverter : System.Text.Json.Serialization.JsonConverter<DateTime?>
{
    /// <inheritdoc/>
    public override DateTime? Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
    {
        return reader switch
        {
            { TokenType: System.Text.Json.JsonTokenType.String } when GetFromSpan(reader, out var value) => value,
            { TokenType: System.Text.Json.JsonTokenType.String } when DateTime.TryParse(reader.GetString(), System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var value) => value,
            _ => reader.GetDateTime(),
        };

        static bool GetFromSpan(System.Text.Json.Utf8JsonReader reader, out DateTime value)
        {
            var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
            return System.Buffers.Text.Utf8Parser.TryParse(span, out value, out var bytesConsumed) && span.Length == bytesConsumed;
        }
    }

    /// <inheritdoc/>
    public override void Write(System.Text.Json.Utf8JsonWriter writer, DateTime? value, System.Text.Json.JsonSerializerOptions options)
    {
    }
}
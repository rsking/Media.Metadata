﻿// -----------------------------------------------------------------------
// <copyright file="PList.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

/// <summary>
/// Represents a PList.
/// </summary>
[XmlRoot(PListElementName)]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is the correct name")]
public class PList : IDictionary<string, object>, IDictionary, IXmlSerializable
{
    private const string ArrayElementName = "array";

    private const string PListElementName = "plist";

    private const string IntegerElementName = "integer";

    private const string RealElementName = "real";

    private const string StringElementName = "string";

    private const string DictionaryElementName = "dict";

    private const string DataElementName = "data";

    private const string DateElementName = "date";

    private const string KeyElementName = "key";

    private const string TrueElementName = "true";

    private const string FalseElementName = "false";

    /// <summary>
    /// Initialises a new instance of the <see cref="PList"/> class.
    /// </summary>
    public PList()
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="PList"/> class.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    internal PList(IDictionary<string, object> dictionary)
    {
        this.Version = new Version(1, 0);
        this.DictionaryImplementation = dictionary;
    }

    /// <inheritdoc />
    public int Count => this.DictionaryImplementation.Count;

    /// <inheritdoc />
    public bool IsReadOnly => this.DictionaryImplementation.IsReadOnly;

    /// <summary>
    /// Gets the version.
    /// </summary>
    public Version? Version { get; private set; }

    /// <inheritdoc />
    public ICollection<string> Keys => this.DictionaryImplementation.Keys;

    /// <inheritdoc />
    public ICollection<object> Values => this.DictionaryImplementation.Values;

    /// <inheritdoc />
    bool IDictionary.IsFixedSize => false;

    /// <inheritdoc />
    bool IDictionary.IsReadOnly => this.IsReadOnly;

    /// <inheritdoc />
    ICollection IDictionary.Keys => (ICollection)this.Keys;

    /// <inheritdoc />
    ICollection IDictionary.Values => (ICollection)this.Values;

    /// <inheritdoc />
    int ICollection.Count => this.Count;

    /// <inheritdoc />
    bool ICollection.IsSynchronized => false;

    /// <inheritdoc />
    object? ICollection.SyncRoot { get; }

    /// <summary>
    /// Gets the implementation.
    /// </summary>
    protected IDictionary<string, object> DictionaryImplementation { get; private set; } = new Dictionary<string, object>(StringComparer.Ordinal);

    /// <inheritdoc />
    public object this[string key]
    {
        get => this.DictionaryImplementation[key];
        set => this.DictionaryImplementation[key] = value;
    }

    /// <inheritdoc />
    object IDictionary.this[object key]
    {
        get => this[key.ToString()];
        set => this[key.ToString()] = value;
    }

    /// <summary>
    /// Creates a new <see cref="PList"/> using the specified string value.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>The created <see cref="PList"/>.</returns>
    public static PList Create(string value)
    {
        using var stream = XmlReader.Create(new StringReader(value), new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore, IgnoreWhitespace = true });
        var serializer = new XmlSerializer(typeof(PList));
        return (PList)serializer.Deserialize(stream);
    }

    /// <summary>
    /// Creates a new <see cref="PList"/> using the specified stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>The created <see cref="PList"/>.</returns>
    public static PList Create(Stream stream)
    {
        using var streamReader = new StreamReader(stream, System.Text.Encoding.UTF8, detectEncodingFromByteOrderMarks: true, 1024, leaveOpen: true);
        using var reader = XmlReader.Create(streamReader, new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore, IgnoreWhitespace = true });
        var serializer = new XmlSerializer(typeof(PList));
        return (PList)serializer.Deserialize(reader);
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => this.DictionaryImplementation.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.DictionaryImplementation).GetEnumerator();

    /// <inheritdoc />
    public void Add(KeyValuePair<string, object> item) => this.DictionaryImplementation.Add(item);

    /// <inheritdoc />
    public void Clear() => this.DictionaryImplementation.Clear();

    /// <inheritdoc />
    public bool Contains(KeyValuePair<string, object> item) => this.DictionaryImplementation.Contains(item);

    /// <inheritdoc />
    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) => this.DictionaryImplementation.CopyTo(array, arrayIndex);

    /// <inheritdoc />
    public bool Remove(KeyValuePair<string, object> item) => this.DictionaryImplementation.Remove(item);

    /// <inheritdoc />
    public void Add(string key, object value) => this.DictionaryImplementation.Add(key, value);

    /// <inheritdoc />
    public bool ContainsKey(string key) => this.DictionaryImplementation.ContainsKey(key);

    /// <inheritdoc />
    public bool Remove(string key) => this.DictionaryImplementation.Remove(key);

    /// <inheritdoc />
    public bool TryGetValue(string key, out object value) => this.DictionaryImplementation.TryGetValue(key, out value);

    /// <inheritdoc />
    public XmlSchema? GetSchema() => null;

    /// <inheritdoc />
    public void ReadXml(XmlReader reader)
    {
        if (reader is null || !string.Equals(reader.Name, PListElementName, StringComparison.Ordinal) || reader.NodeType != XmlNodeType.Element)
        {
            return;
        }

        this.Version = Version.Parse(reader.GetAttribute("version"));

        // read through the reader
        if (!ReadWhileWhiteSpace(reader))
        {
            return;
        }

        // read through the dictionary
        if (!string.Equals(reader.Name, DictionaryElementName, StringComparison.Ordinal))
        {
            return;
        }

        this.DictionaryImplementation = ReadDictionary(reader);

        if (ReadWhileWhiteSpace(reader) && string.Equals(reader.Name, PListElementName, StringComparison.Ordinal) && reader.NodeType == XmlNodeType.EndElement)
        {
            return;
        }

        throw new ArgumentException(Properties.Resources.InvalidXml, nameof(reader));
    }

    /// <inheritdoc />
    public override string ToString()
    {
        using var writer = new Utf8StringWriter();
        using var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Encoding = writer.Encoding });
        xmlWriter.WriteDocType("plist", "-//Apple Computer//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", subset: null);
        var serializer = new XmlSerializer(typeof(PList));
        serializer.Serialize(xmlWriter, this);
        return writer.ToString();
    }

    /// <inheritdoc />
    public void WriteXml(XmlWriter writer)
    {
        if (writer is null)
        {
            throw new ArgumentNullException(nameof(writer));
        }

        writer.WriteAttributeString("version", "1.0");
        WriteDictionary(writer, 0, this);
    }

    /// <inheritdoc/>
    void IDictionary.Add(object key, object value) => this.Add(key.ToString(), value);

    /// <inheritdoc/>
    void IDictionary.Clear() => this.Clear();

    /// <inheritdoc/>
    bool IDictionary.Contains(object key) => this.ContainsKey(key.ToString());

    /// <inheritdoc/>
    IDictionaryEnumerator IDictionary.GetEnumerator() => ((IDictionary)this.DictionaryImplementation).GetEnumerator();

    /// <inheritdoc/>
    void IDictionary.Remove(object key) => this.Remove(key.ToString());

    /// <inheritdoc/>
    void ICollection.CopyTo(Array array, int index) => ((IDictionary)this.DictionaryImplementation).CopyTo(array, index);

    private static IDictionary<string, object> ReadDictionary(XmlReader reader)
    {
        IDictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
        string? key = null;
        object? value = null;

        while (ReadWhileWhiteSpace(reader))
        {
            if (string.Equals(reader.Name, DictionaryElementName, StringComparison.Ordinal) && reader.NodeType == XmlNodeType.EndElement)
            {
                return dictionary;
            }

            if (string.Equals(reader.Name, KeyElementName, StringComparison.Ordinal))
            {
                _ = ReadWhileWhiteSpace(reader);
                if (key is not null)
                {
                    AddToDictionary();
                }

                key = reader.Value;
                _ = ReadWhileWhiteSpace(reader);
                continue;
            }

            value = ReadValue(reader);
            AddToDictionary();

            void AddToDictionary()
            {
                dictionary.Add(key!, value!);
                key = null;
                value = null;
            }
        }

        return dictionary;
    }

    private static object? ReadValue(XmlReader reader)
    {
        return reader.Name switch
        {
            TrueElementName => true,
            FalseElementName => false,
            IntegerElementName => ReadInteger(reader),
            RealElementName => ReadDouble(reader),
            StringElementName => ReadString(reader),
            DateElementName => ReadDate(reader),
            DictionaryElementName => ReadDictionary(reader),
            ArrayElementName => ReadArray(reader),
            DataElementName => ReadData(reader),
            _ => throw new ArgumentException(Properties.Resources.InvalidPListValueType, nameof(reader)),
        };

        static long ReadInteger(XmlReader reader)
        {
            _ = ReadWhileWhiteSpace(reader);
            var longValue = long.Parse(reader.Value, CultureInfo.InvariantCulture);
            _ = ReadWhileWhiteSpace(reader);
            return longValue;
        }

        static double ReadDouble(XmlReader reader)
        {
            _ = ReadWhileWhiteSpace(reader);
            var doubleValue = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            _ = ReadWhileWhiteSpace(reader);
            return doubleValue;
        }

        static string? ReadString(XmlReader reader)
        {
            // read until the end
            var builder = new System.Text.StringBuilder();
            var first = true;
            while (ReadWhileWhiteSpace(reader))
            {
                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    return builder.ToString();
                }

                if (!first)
                {
                    _ = builder.AppendLine();
                }

                first = false;
                _ = builder.Append(reader.Value);
            }

            return first ? null : builder.ToString();
        }

        static DateTime ReadDate(XmlReader reader)
        {
            _ = ReadWhileWhiteSpace(reader);
            var dateValue = DateTime.Parse(reader.Value, CultureInfo.InvariantCulture);
            _ = ReadWhileWhiteSpace(reader);
            return dateValue;
        }

        static object?[] ReadArray(XmlReader reader)
        {
            var list = new List<object?>();

            while (ReadWhileWhiteSpace(reader))
            {
                if (string.Equals(reader.Name, ArrayElementName, StringComparison.Ordinal) && reader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }

                list.Add(ReadValue(reader));
            }

            return list.ToArray();
        }

        static byte[] ReadData(XmlReader reader)
        {
            _ = ReadWhileWhiteSpace(reader);
            var dataValue = Convert.FromBase64String(reader.Value);
            _ = ReadWhileWhiteSpace(reader);
            return dataValue;
        }
    }

    private static void WriteDictionary(XmlWriter writer, int indentLevel, IDictionary dictionary)
    {
        writer.WriteWhitespace(new string('\t', indentLevel));
        writer.WriteStartElement(DictionaryElementName);
        writer.WriteWhitespace(Environment.NewLine);

        var indent = new string('\t', indentLevel + 1);
        foreach (var key in dictionary.Keys)
        {
            writer.WriteWhitespace(indent);
            writer.WriteElementString(KeyElementName, key.ToString());
            WriteValue(writer, indentLevel + 1, dictionary[key]);
            writer.WriteWhitespace(Environment.NewLine);
        }

        writer.WriteWhitespace(new string('\t', indentLevel));
        writer.WriteEndElement();
    }

    private static void WriteValue(XmlWriter writer, int indentLevel, object value)
    {
        var type = value.GetType();
        if (type == typeof(bool))
        {
            var boolValue = (bool)value;
            writer.WriteStartElement(boolValue ? TrueElementName : FalseElementName);
            writer.WriteEndElement();
        }
        else if (type == typeof(int) || type == typeof(long))
        {
            var longValue = (long)value;
            writer.WriteElementString(IntegerElementName, longValue.ToString(CultureInfo.InvariantCulture));
        }
        else if (type == typeof(float) || type == typeof(double))
        {
            var doubleValue = (double)value;
            writer.WriteElementString(RealElementName, doubleValue.ToString(CultureInfo.InvariantCulture));
        }
        else if (type == typeof(string))
        {
            var stringValue = (string)value;
            writer.WriteElementString(StringElementName, stringValue);
        }
        else if (type == typeof(DateTime))
        {
            var dateValue = (DateTime)value;
            writer.WriteElementString(DateElementName, dateValue.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture) + "Z");
        }
        else if (typeof(IDictionary).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
        {
            // put the dictionary on a new line.
            writer.WriteWhitespace(Environment.NewLine);
            WriteDictionary(writer, indentLevel, (IDictionary)value);
        }
        else if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
        {
            var arrayValue = (IEnumerable)value;
            writer.WriteStartElement(ArrayElementName);
            foreach (var item in arrayValue)
            {
                WriteValue(writer, indentLevel, item);
            }

            writer.WriteEndElement();
        }
        else if (type == typeof(byte[]))
        {
            var byteValue = (byte[])value;
            writer.WriteElementString(DataElementName, Convert.ToBase64String(byteValue));
        }
    }

    private static bool ReadWhileWhiteSpace(XmlReader reader)
    {
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Whitespace)
            {
                continue;
            }

            return true;
        }

        return false;
    }

    private sealed class Utf8StringWriter : StringWriter
    {
        public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;
    }
}
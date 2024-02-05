// -----------------------------------------------------------------------
// <copyright file="NativeMethods.Itmf.Generic.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <content>
/// Methods from <c>itmf_generic.h</c>.
/// </content>
internal static partial class NativeMethods
{
    /// <summary>
    /// Represents the iTunes Metadata Format basic types.
    /// </summary>
    /// <remarks>
    /// These values are taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// Basic types of value data as enumerated in spec. */
    /// typedef enum MP4ItmfBasicType_e
    /// {
    ///     MP4_ITMF_BT_IMPLICIT  = 0,   /** for use with tags for which no type needs to be indicated */
    ///     MP4_ITMF_BT_UTF8      = 1,   /** without any count or null terminator */
    ///     MP4_ITMF_BT_UTF16     = 2,   /** also known as UTF-16BE */
    ///     MP4_ITMF_BT_SJIS      = 3,   /** deprecated unless it is needed for special Japanese characters */
    ///     MP4_ITMF_BT_HTML      = 6,   /** the HTML file header specifies which HTML version */
    ///     MP4_ITMF_BT_XML       = 7,   /** the XML header must identify the DTD or schemas */
    ///     MP4_ITMF_BT_UUID      = 8,   /** also known as GUID; stored as 16 bytes in binary (valid as an ID) */
    ///     MP4_ITMF_BT_ISRC      = 9,   /** stored as UTF-8 text (valid as an ID) */
    ///     MP4_ITMF_BT_MI3P      = 10,  /** stored as UTF-8 text (valid as an ID) */
    ///     MP4_ITMF_BT_GIF       = 12,  /** (deprecated) a GIF image */
    ///     MP4_ITMF_BT_JPEG      = 13,  /** a JPEG image */
    ///     MP4_ITMF_BT_PNG       = 14,  /** a PNG image */
    ///     MP4_ITMF_BT_URL       = 15,  /** absolute, in UTF-8 characters */
    ///     MP4_ITMF_BT_DURATION  = 16,  /** in milliseconds, 32-bit integer */
    ///     MP4_ITMF_BT_DATETIME  = 17,  /** in UTC, counting seconds since midnight, January 1, 1904; 32 or 64-bits */
    ///     MP4_ITMF_BT_GENRES    = 18,  /** a list of enumerated values */
    ///     MP4_ITMF_BT_INTEGER   = 21,  /** a signed big-endian integer with length one of { 1,2,3,4,8 } bytes */
    ///     MP4_ITMF_BT_RIAA_PA   = 24,  /** RIAA parental advisory; { -1=no, 1=yes, 0=unspecified }, 8-bit ingteger */
    ///     MP4_ITMF_BT_UPC       = 25,  /** Universal Product Code, in text UTF-8 format (valid as an ID) */
    ///     MP4_ITMF_BT_BMP       = 27,  /** Windows bitmap image */
    ///     MP4_ITMF_BT_UNDEFINED = 255  /** undefined */
    /// } MP4ItmfBasicType;
    /// </code>
    /// </para>
    /// </remarks>
    public enum MP4ItmfBasicType
    {
        /// <summary>
        /// For use with tags for which no type needs to be indicated.
        /// </summary>
        Implicit = 0,

        /// <summary>
        /// Without any count or null terminator.
        /// </summary>
        Utf8 = 1,

        /// <summary>
        /// Also known as UTF-16BE.
        /// </summary>
        Utf16 = 2,

        /// <summary>
        /// Deprecated unless it is needed for special Japanese characters.
        /// </summary>
        Sjis = 3,

        /// <summary>
        /// The HTML file header specifies which HTML version.
        /// </summary>
        Html = 6,

        /// <summary>
        /// The XML header must identify the DTD or schemas.
        /// </summary>
        Xml = 7,

        /// <summary>
        /// Also known as GUID; stored as 16 bytes in binary (valid as an ID).
        /// </summary>
        Uuid = 8,

        /// <summary>
        /// stored as UTF-8 text (valid as an ID).
        /// </summary>
        Isrc = 9,

        /// <summary>
        /// stored as UTF-8 text (valid as an ID).
        /// </summary>
        Mi3p = 10,

        /// <summary>
        /// (deprecated) a GIF image.
        /// </summary>
        Gif = 12,

        /// <summary>
        /// a JPEG image.
        /// </summary>
        Jpeg = 13,

        /// <summary>
        /// A PNG image.
        /// </summary>
        Png = 14,

        /// <summary>
        /// absolute, in UTF-8 characters.
        /// </summary>
        Url = 15,

        /// <summary>
        /// in milliseconds, 32-bit integer.
        /// </summary>
        Duration = 16,

        /// <summary>
        /// in UTC, counting seconds since midnight, January 1, 1904; 32 or 64-bits.
        /// </summary>
        DateTime = 17,

        /// <summary>
        /// a list of enumerated values.
        /// </summary>
        Genres = 18,

        /// <summary>
        /// a signed big-endian integer with length one of { 1,2,3,4,8 } bytes.
        /// </summary>
        Integer = 21,

        /// <summary>
        /// RIAA parental advisory; { -1=no, 1=yes, 0=unspecified }, 8-bit integer.
        /// </summary>
        Riaa_pa = 24,

        /// <summary>
        /// Universal Product Code, in text UTF-8 format (valid as an ID).
        /// </summary>
        Upc = 25,

        /// <summary>
        /// A Windows bitmap image.
        /// </summary>
        Bmp = 27,

        /// <summary>
        /// An undefined value.
        /// </summary>
        Undefined = 255,
    }

    /// <summary>
    /// Get list of items by meaning from file.
    /// </summary>
    /// <param name="file">handle of file to operate on.</param>
    /// <param name="meaning">UTF-8 meaning. NULL-terminated.</param>
    /// <param name="name">may be NULL. UTF-8 name. NULL-terminated.</param>
    /// <returns>On succes, list of items, which must be free'd. On failure, <see cref="IntPtr.Zero"/>.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr MP4ItmfGetItemsByMeaning(IntPtr file, [MarshalAs(UnmanagedType.LPStr)] string meaning, [MarshalAs(UnmanagedType.LPStr)] string? name);

    /// <summary>
    /// Get list of items by code from file.
    /// </summary>
    /// <param name="file">handle of file to operate on.</param>
    /// <param name="code">four-char code identifying atom type. NULL-terminated.</param>
    /// <returns>On succes, list of items, which must be free'd. On failure, <see cref="IntPtr.Zero"/>.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr MP4ItmfGetItemsByCode(IntPtr file, [MarshalAs(UnmanagedType.LPStr)] string code);

    /// <summary>
    /// Free an item list (deep free).
    /// </summary>
    /// <param name="itemList">itemList to be free'd.</param>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern void MP4ItmfItemListFree(IntPtr itemList);

    /// <summary>
    /// Allocate an item on the heap.
    /// </summary>
    /// <param name="code">four-char code identifying atom type. NULL-terminated.</param>
    /// <param name="numData">number of data elements to allocate. Must be >= 1.</param>
    /// <returns>newly allocated item.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr MP4ItmfItemAlloc([MarshalAs(UnmanagedType.LPStr)] string code, int numData);

    /// <summary>
    /// Add an item to file.
    /// </summary>
    /// <param name="hFile">handle of file to operate on.</param>
    /// <param name="item">object to add.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4ItmfAddItem(IntPtr hFile, IntPtr item);

    //// Commenting this API declaration. It isn't called yet, but may be in the future.
    //// [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    //// [return: MarshalAs(UnmanagedType.U1)]
    //// public static extern bool MP4ItmfSetItem(IntPtr hFile, IntPtr item);

    /// <summary>
    /// Remove an existing item from file.
    /// </summary>
    /// <param name="hFile">handle of file to operate on.</param>
    /// <param name="item">object to remove. Must have a valid index obtained from prior get.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4ItmfRemoveItem(IntPtr hFile, IntPtr item);

    /// <summary>
    /// Models an iTunes Metadata Format data atom contained in an iTMF metadata item atom.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// typedef struct MP4ItmfData_s
    /// {
    ///     uint8_t          typeSetIdentifier; /** always zero. */
    ///     MP4ItmfBasicType typeCode;          /** iTMF basic type. */
    ///     uint32_t         locale;            /** always zero. */
    ///     uint8_t*         value;             /** may be NULL. */
    ///     uint32_t         valueSize;         /** value size in bytes. */
    /// } MP4ItmfData;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MP4ItmfData
    {
        /// <summary>
        /// Always zero.
        /// </summary>
        public byte typeSetIdentifier;

        /// <summary>
        /// Basic type of data.
        /// </summary>
        public MP4ItmfBasicType typeCode;

        /// <summary>
        /// Always zero.
        /// </summary>
        public int locale;

        /// <summary>
        /// Value of the data, may be NULL (<see cref="IntPtr.Zero"/>).
        /// </summary>
        public IntPtr value;

        /// <summary>
        /// Value size in bytes.
        /// </summary>
        public int valueSize;
    }

    /// <summary>
    /// Represents a list of data in an atom.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// List of data. */
    /// typedef struct MP4ItmfDataList_s
    /// {
    ///     MP4ItmfData* elements; /** flat array. NULL when size is zero. */
    ///     uint32_t     size;     /** number of elements. */
    /// } MP4ItmfDataList;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MP4ItmfDataList
    {
        /// <summary>
        /// flat array. NULL when size is zero.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray)]
        public IntPtr[] elements;

        /// <summary>
        /// number of elements.
        /// </summary>
        public int size;
    }

    /// <summary>
    /// Models an iTMF metadata item atom contained in an iTunes atom.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// typedef struct MP4ItmfItem_s
    /// {
    ///     void* __handle; /** internal use only. */
    ///
    ///     char*           code;     /** four-char code identifing atom type. NULL-terminated. */
    ///     char*           mean;     /** may be NULL. UTF-8 meaning. NULL-terminated. */
    ///     char*           name;     /** may be NULL. UTF-8 name. NULL-terminated. */
    ///     MP4ItmfDataList dataList; /** list of data. can be zero length. */
    /// } MP4ItmfItem;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MP4ItmfItem
    {
        /// <summary>
        /// internal use only.
        /// </summary>
        public IntPtr handle;

        /// <summary>
        /// four-char code identifying atom type. NULL-terminated.
        /// </summary>
        public string code;

        /// <summary>
        /// may be NULL. UTF-8 meaning. NULL-terminated.
        /// </summary>
        public string mean;

        /// <summary>
        /// may be NULL. UTF-8 name. NULL-terminated.
        /// </summary>
        public string name;

        /// <summary>
        /// list of data. can be zero length.
        /// </summary>
        public MP4ItmfDataList dataList;
    }

    /// <summary>
    /// List of items.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// typedef struct MP4ItmfItemList_s
    /// {
    ///     MP4ItmfItem* elements; /** flat array. NULL when size is zero. */
    ///     uint32_t     size;     /** number of elements. */
    /// } MP4ItmfItemList;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MP4ItmfItemList
    {
        /// <summary>
        /// flat array. NULL when size is zero.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray)]
        public IntPtr[] elements;

        /// <summary>
        /// number of elements.
        /// </summary>
        public int size;
    }
}
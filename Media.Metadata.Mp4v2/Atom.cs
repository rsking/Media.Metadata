// -----------------------------------------------------------------------
// <copyright file="Atom.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The <see cref="Atom"/> class is the base class for so-called "reverse-DNS" MP4 tag atoms.
/// These are supported only by a low-level API in the MP4V2 library, and as such, require special handling to read and write.
/// </summary>
internal abstract class Atom
{
    /// <summary>
    /// Gets the data type of the data stored in the atom.
    /// </summary>
    public virtual NativeMethods.MP4ItmfBasicType DataType => NativeMethods.MP4ItmfBasicType.Utf8;

    /// <summary>
    /// Gets the meaning of the atom.
    /// </summary>
    public abstract string Meaning { get; }

    /// <summary>
    /// Gets the name of the atom.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Initializes the <see cref="Atom"/> instance from the specified <see cref="IntPtr"/>
    /// value.
    /// </summary>
    /// <param name="fileHandle">The <see cref="IntPtr"/> file handle of the file from which to read this <see cref="Atom"/>.</param>
    /// <returns><see langword="true"/> if this <see cref="Atom"/> was successfully initialized; otherwise, <see langword="false"/>.</returns>
    public bool Initialize(IntPtr fileHandle)
    {
        var isInitialized = false;
        var rawAtomPointer = NativeMethods.MP4ItmfGetItemsByMeaning(fileHandle, this.Meaning, this.Name);
        if (rawAtomPointer != IntPtr.Zero)
        {
            var atomItemList = rawAtomPointer.ToStructure<NativeMethods.MP4ItmfItemList>();
            isInitialized = atomItemList.size > 0;
            for (var i = 0; i < atomItemList.size; i++)
            {
                var itemPointer = atomItemList.elements[i];
                var item = itemPointer.ToStructure<NativeMethods.MP4ItmfItem>();
                var dataList = item.dataList;
                for (var j = 0; j < dataList.size; j++)
                {
                    var dataListItemPointer = dataList.elements[j];
                    var data = dataListItemPointer.ToStructure<NativeMethods.MP4ItmfData>();
                    if (data.typeCode == this.DataType && data.value.ToByteArray(data.valueSize) is { } dataBuffer)
                    {
                        this.Populate(dataBuffer);
                    }
                }
            }

            NativeMethods.MP4ItmfItemListFree(rawAtomPointer);
        }

        return isInitialized;
    }

    /// <summary>
    /// Populates this <see cref="Atom"/> with the specific data stored in it.
    /// </summary>
    /// <param name="dataBuffer">A byte array containing the iTunes Metadata Format data
    /// used to populate this <see cref="Atom"/>.</param>
    public abstract void Populate(byte[] dataBuffer);

    /// <summary>
    /// Returns the data to be stored in this <see cref="Atom"/> as a byte array.
    /// </summary>
    /// <returns>The byte array containing the data to be stored in the atom.</returns>
    public abstract byte[] ToByteArray();
}
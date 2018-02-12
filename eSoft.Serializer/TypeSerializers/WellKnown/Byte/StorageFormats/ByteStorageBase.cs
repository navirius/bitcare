// -----------------------------------------------------------------------
// <copyright file="Int32ConfigCaseBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.StorageFormat;

namespace eSoft.Serializer.TypeSerializers.WellKnown.Byte_StorageFormats
{
    // Config cases
    public enum ByteStorageFormats { DefaultValue, ValueInDataStream }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ByteStorageBase : FormatBase, IStorageFormat
    {
        // Size of Id value in bits
        public const byte FormatIdSizeInBits = 1;

        // Constructor that requires config case value
        public ByteStorageBase(ByteStorageFormats confCase, byte usedConfigBits): base((byte)confCase, FormatIdSizeInBits, usedConfigBits) { }

        // Casted config case value
        public ByteStorageFormats FormatType { get { return (ByteStorageFormats)FormatId; } }
    }
}

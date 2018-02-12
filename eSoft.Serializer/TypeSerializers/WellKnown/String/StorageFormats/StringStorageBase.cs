// -----------------------------------------------------------------------
// <copyright file="StringStorageCaseBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.StorageFormat;

namespace eSoft.Serializer.TypeSerializers.WellKnown.String_StorageFormats
{
    // Storage cases for string
    public enum StringStorageFormats { NullString, EmptyString, NormalString, CachedString }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class StringStorageBase : FormatBase, IStorageFormat
    {
        // Size of Id value in bits
        public const byte FormatIdSizeInBits = 2;

        // Constructor that requires config case value
        public StringStorageBase(StringStorageFormats confCase, byte usedConfigBits): base((byte)confCase, FormatIdSizeInBits, usedConfigBits) { }

        // Casted config case value
        public StringStorageFormats FormatType { get { return (StringStorageFormats)FormatId; } }
    }
}

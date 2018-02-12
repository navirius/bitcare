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

namespace eSoft.Serializer.TypeSerializers.ComplexType_StorageFormats
{
    // Storage cases for string
    public enum ComplexTypeStorageFormats { NullObject, NormalObject, CachedObject }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ComplexTypeStorageBase : FormatBase, IStorageFormat
    {
        // Size of Id value in bits
        public const byte FormatIdSizeInBits = 2;

        // Constructor that requires config case value
        public ComplexTypeStorageBase(ComplexTypeStorageFormats confCase, byte usedConfigBits) : base((byte)confCase, FormatIdSizeInBits, usedConfigBits) { }

        // Casted config case value
        public ComplexTypeStorageFormats FormatType { get { return (ComplexTypeStorageFormats)FormatId; } }
    }
}

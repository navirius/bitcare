// -----------------------------------------------------------------------
// <copyright file="Int16ConfigCaseBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.StorageFormat;

namespace eSoft.Serializer.TypeSerializers.WellKnown.Int16_StorageFormats
{
    // Config cases
    public enum Int16StorageFormats { DefaultValue, ValueInConfig, ValueInDataStream }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Int16StorageBase : FormatBase, IStorageFormat
    {
        // Size of Id value in bits
        public const byte FormatIdSizeInBits = 2;

        // Constructor that requires config case value
        public Int16StorageBase(Int16StorageFormats confCase, byte usedConfigBits): base((byte)confCase, FormatIdSizeInBits, usedConfigBits) { }

        // Casted config case value
        public Int16StorageFormats FormatType { get { return (Int16StorageFormats)FormatId; } }
    }
}

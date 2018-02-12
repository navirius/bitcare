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

namespace eSoft.Serializer.TypeSerializers.WellKnown.Int32_StorageFormats
{
    // Config cases
    public enum Int32StorageFormats { DefaultValue, ValueInConfig, ValueInDataStream }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Int32StorageBase : FormatBase, IStorageFormat
    {
        // Size of Id value in bits
        public const byte FormatIdSizeInBits = 2;

        // Constructor that requires config case value
        public Int32StorageBase(Int32StorageFormats confCase, byte usedConfigBits): base((byte)confCase, FormatIdSizeInBits, usedConfigBits) { }

        // Casted config case value
        public Int32StorageFormats FormatType { get { return (Int32StorageFormats)FormatId; } }
    }
}

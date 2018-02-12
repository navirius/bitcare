// -----------------------------------------------------------------------
// <copyright file="DoubleConfigCaseBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.StorageFormat;

namespace eSoft.Serializer.TypeSerializers.WellKnown.Double_StorageFormats
{
    // Config cases
    public enum DoubleStorageFormats { DefaultValue, ValueInDataStream }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DoubleStorageBase : FormatBase, IStorageFormat
    {
        // Size of Id value in bits
        public const byte FormatIdSizeInBits = 1;

        // Constructor that requires config case value
        public DoubleStorageBase(DoubleStorageFormats confCase, byte usedConfigBits): base((byte)confCase, FormatIdSizeInBits, usedConfigBits) { }

        // Casted config case value
        public DoubleStorageFormats FormatType { get { return (DoubleStorageFormats)FormatId; } }
    }
}

// -----------------------------------------------------------------------
// <copyright file="DecimalConfigCaseBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.StorageFormat;

namespace eSoft.Serializer.TypeSerializers.WellKnown.Decimal_StorageFormats
{
    // Config cases
    public enum DecimalStorageFormats { DefaultValue, PositiveValueInDataStream, NegativeValueInDataStream }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DecimalStorageBase : FormatBase, IStorageFormat
    {
        // Size of Id value in bits
        public const byte FormatIdSizeInBits = 2;

        // Constructor that requires config case value
        public DecimalStorageBase(DecimalStorageFormats confCase, byte usedConfigBits) : base((byte)confCase, FormatIdSizeInBits, usedConfigBits) { }

        // Casted config case value
        public DecimalStorageFormats FormatType { get { return (DecimalStorageFormats)FormatId; } }
    }
}

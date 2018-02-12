// -----------------------------------------------------------------------
// <copyright file="ValueInDataStream.cs" company="">
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
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PositiveValueInDataStream : DecimalStorageBase
    {
        // Length of data in PackedData store
        public const int UsedConfigBitsForCase = 4;

        // Default constructor
        public PositiveValueInDataStream() : base(DecimalStorageFormats.PositiveValueInDataStream, UsedConfigBitsForCase) { }

        // Storage constructor
        public PositiveValueInDataStream(byte packedDataSize)
            : this()
        {
            PackedDataSize = packedDataSize;
        }

        // When we have value embedded in config then all the bits in config contains stored value. 0 is encoded as special value so we can store value 1-16 (as 0-15)
        public int PackedDataSize
        {
            get { return (int)FormatConfig.Bits + 1; }
            set { FormatConfig.Bits = (uint)value - 1; }
        }
    }
}

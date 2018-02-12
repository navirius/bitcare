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

namespace eSoft.Serializer.TypeSerializers.WellKnown.Int32_StorageFormats
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ValueInDataStream : Int32StorageBase
    {
        // We use 2 bits to inform on how many bytes we stored Int32 value (it can be from 1 to 4 bytes)
        public const int UsedConfigBitsForCase = 2;

        // Default constructor
        public ValueInDataStream() : base(Int32StorageFormats.ValueInDataStream, UsedConfigBitsForCase) { }

        // Storage constructor
        public ValueInDataStream(byte packedDataSize) : this()
        {
            PackedDataSize = packedDataSize;
        }

        // When we have value embedded in config then all the bits in config contains stored value. 0 is encoded as special value so we can store value 1-4 (as 0-3)
        public byte PackedDataSize
        {
            get { return (byte)(FormatConfig.Bits + 1); }
            set { FormatConfig.Bits = (uint)value - 1; }
        }
    }
}

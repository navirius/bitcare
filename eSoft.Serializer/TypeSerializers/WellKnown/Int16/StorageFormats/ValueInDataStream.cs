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

namespace eSoft.Serializer.TypeSerializers.WellKnown.Int16_StorageFormats
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ValueInDataStream : Int16StorageBase
    {
        // We use 2 bits to inform on how many bytes we stored Int16 value (it can be from 1 to 2 bytes)
        public const int UsedConfigBitsForCase = 1;

        // Default constructor
        public ValueInDataStream() : base(Int16StorageFormats.ValueInDataStream, UsedConfigBitsForCase) { }

        // Storage constructor
        public ValueInDataStream(byte packedDataSize) : this()
        {
            PackedDataSize = packedDataSize;
        }

        // When we have value embedded in config then all the bits in config contains stored value. 0 is encoded as special value so we can store value 1-2 (as 0-1)
        public byte PackedDataSize
        {
            get { return (byte)(FormatConfig.Bits + 1); }
            set { FormatConfig.Bits = (uint)value - 1; }
        }
    }
}

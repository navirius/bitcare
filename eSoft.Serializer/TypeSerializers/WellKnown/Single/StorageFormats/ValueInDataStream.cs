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

namespace eSoft.Serializer.TypeSerializers.WellKnown.Single_StorageFormats
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ValueInDataStream : SingleStorageBase
    {
        public const int UsedConfigBitsForCase = 4;

        // Default constructor
        public ValueInDataStream(byte byteMap) : base(SingleStorageFormats.ValueInDataStream, UsedConfigBitsForCase) { ByteMap = byteMap; }

        // When we have value embedded in config then all the bits in config contains stored value.
        public byte ByteMap
        {
            get { return (byte)(FormatConfig.Bits); }
            set { FormatConfig.Bits = (uint)value; }
        }
    }
}

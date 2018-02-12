// -----------------------------------------------------------------------
// <copyright file="ValueInConfig.cs" company="">
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
    public class ValueInConfig : Int32StorageBase
    {
        public const int UsedConfigBitsForValue = 8;    // We use up to 8 bits (1 byte) for internal storage
        public const int MaxValueToStoreInConfig = 0xff;  // 0 is default value so we add 1 to stored value (should be substracted then)
        
        // Default constructor
        public ValueInConfig() : base(Int32StorageFormats.ValueInConfig, UsedConfigBitsForValue) { }

        // Storage constructor
        public ValueInConfig(int valueToStore) : this() 
        {
            Value = valueToStore;
        }

        // When we have value embedded in config then all the bits in config contains stored value. 0 is encoded as special value so we can store value 1-64 (as 0-63)
        public int Value { 
            get { return (int)FormatConfig.Bits + 1; }
            set { FormatConfig.Bits = (uint)value - 1; }
        }
    }
}

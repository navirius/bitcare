using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Compression;
using System.IO;
using eSoft.Serializer.Infrastructure.Helpers;

namespace eSoft.Serializer
{
    // Int32 serialization
    public partial class Serializer
    {
        // Serialization of Int32
        public static byte[] SerializeDateTime(DateTime valueToSerialize)
        {
            //Int64 value = valueToSerialize.ToBinary();
            Int64 value = valueToSerialize.Ticks;
            return Serializer.SerializeInt64(value);
        }

        // Deserialization of Int32
        public static DateTime DeserializeDateTime(byte[] serializedData)
        {
            Int64 value = DeserializeInt64(serializedData);
            //return DateTime.FromBinary(value);
            return new DateTime(value);
        }
    }
}

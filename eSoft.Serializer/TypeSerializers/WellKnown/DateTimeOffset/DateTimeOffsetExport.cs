using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Compression;
using System.IO;
using eSoft.Serializer.Infrastructure.Helpers;

namespace eSoft.Serializer
{
    // DateTimeOffset serialization
    public partial class Serializer
    {
        // Serialization of DateTimeOffset
        public static byte[] SerializeDateTimeOffset(DateTimeOffset valueToSerialize)
        {
            return Serializer.SerializeInt64(valueToSerialize.Ticks);
        }

        // Deserialization of DateTimeOffset
        public static DateTimeOffset DeserializeDateTimeOffset(byte[] serializedData)
        {
            return new DateTimeOffset(DeserializeInt64(serializedData), TimeSpan.Zero);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Compression;
using System.IO;
using eSoft.Serializer.Infrastructure.Helpers;

namespace eSoft.Serializer
{
    public partial class Serializer
    {
        // Serialization
        public static byte[] SerializeByte(Byte valueToSerialize)
        {
            return new byte[1] { valueToSerialize };
        }

        // Deserialization
        public static Byte DeserializeByte(byte[] serializedData)
        {
            return serializedData[0];
        }
    }
}

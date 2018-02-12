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
        public static byte[] SerializeChar(Char valueToSerialize)
        {
            // Return result
            return SerializeByte(Convert.ToByte(valueToSerialize));
        }

        // Deserializatio
        public static Char DeserializeChar(byte[] serializedData)
        {
            return Convert.ToChar(DeserializeByte(serializedData));
        }
    }
}

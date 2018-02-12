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
        // Serialization
        public static byte[] SerializeBoolean(Boolean valueToSerialize)
        {
            // Value for true
            if (valueToSerialize)
                return new byte[1] { 1 };

            // Value for false
            return new byte[1] { 0 };
        }

        // Deserialization of Int32
        public static Boolean DeserializeBoolean(byte[] serializedData)
        {
            // Value for true
            if (serializedData[0] == 1)
                return true;

            // Value for false
            return false;
        }
    }
}

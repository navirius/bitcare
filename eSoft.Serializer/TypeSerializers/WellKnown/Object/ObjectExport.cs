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
        public static byte[] SerializeObject(Object valueToSerialize)
        {
            // Is Object created?
            if (valueToSerialize!=null)
                return new byte[1] { 1 };

            // Null value
            return new byte[1] { 0 };
        }

        // Deserialization of Int32
        public static Object DeserializeObject(byte[] serializedData)
        {
            // Object instance
            if (serializedData[0] == 1)
                return new object();

            // Null value
            return null;
        }
    }
}

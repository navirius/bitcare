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
        // Serialization of Double
        public static byte[] SerializeDouble(Double valueToSerialize)
        {
            // Default value has byte 0 only
            if (valueToSerialize == 0F)
                return new byte[1] { 0 };

            // Value different then default one
            return BitToolkit.ConvertDoubleToByteArray(valueToSerialize);
        }

        // Deserialization of Double
        public static Double DeserializeDouble(byte[] serializedData)
        {
            // Default value has byte 0 only
            if (serializedData.Length == 1 && serializedData[0] == 0)
                return 0F;

            // Value different then default one
            return BitToolkit.ConvertByteArrayToDouble(serializedData);
        }
    }
}

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
        // Serialization of Guid
        public static byte[] SerializeGuid(Guid valueToSerialize)
        {
            // Default value has byte 0 only
            if (valueToSerialize == Guid.Empty)
                return new byte[1] { 0 };

            // Value different then default one
            return BitToolkit.ConvertGuidToByteArray(valueToSerialize);
        }

        // Deserialization of Guid
        public static Guid DeserializeGuid(byte[] serializedData)
        {
            // Default value has byte 0 only
            if (serializedData.Length == 1 && serializedData[0] == 0)
                return Guid.Empty;

            // Value different then default one
            return BitToolkit.ConvertByteArrayToGuid(serializedData);
        }
    }
}

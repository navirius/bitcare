using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Compression;
using System.IO;

namespace eSoft.Serializer
{
    // Int32 serialization
    public partial class Serializer
    {
        // Serialization of Int32
        public static byte[] SerializeString(String valueToSerialize)
        {
            // 2 bits for config (lsb)
            // 00 - has null value
            // 01 - is empty string
            // 10 - stored after config byte - then next element is utf8 encoded string (string has not been compressed)
            // 11 - stored after config byte - then next element is utf8 encoded string (string has been compressed) 


            // Case: 00 - null value
            if (valueToSerialize == null)
                return new byte[1] { 0 };

            // Case 01 - empty string
            if (valueToSerialize=="")
                return new byte[1] { 1 };

            // String conversion to byte[]
            byte[] encodedString = Encoding.UTF8.GetBytes(valueToSerialize);
            byte[] compressedString = new InternalCompression().Compress(encodedString);

            // Buffer for data
            List<byte> result = new List<byte>(); 

            // If compressed stream is lower
            if (compressedString.Length < encodedString.Length)
            {
                // Case 11 - compressed string
                result.Add(3);
                result.AddRange(compressedString);
            }
            else
            {
                // Case 10 - encoded to utf8 only string
                result.Add(2);
                result.AddRange(encodedString);
            }

            // Return result
            return result.ToArray();
        }

        // Deserialization of Int32
        public static String DeserializeString(byte[] serializedData)
        {
            // 2 bits for config (lsb)
            // 00 - has null value
            // 01 - is empty string
            // 10 - stored after config byte - then next element is utf8 encoded string (string has not been compressed)
            // 11 - stored after config byte - then next element is utf8 encoded string (string has been compressed) 


            // Read config byte
            byte config = serializedData[0];

            // Is it null string
            if (config == 0)
                return null;

            // Is it null string
            if (config == 1)
                return "";

            // Is it utf8 encoded string only?
            if (config == 2)
                return Encoding.UTF8.GetString(serializedData, 1, serializedData.Length - 1);

            // It's utf8 encoded and compressed string

            // Copy string data
            byte[] stringData = new byte[serializedData.Length - 1];
            Array.Copy(serializedData, 1, stringData, 0, stringData.Length);

            // Temporary array
            byte[] tmpResult = new InternalCompression().Decompress(stringData);

            // Return result
            return Encoding.UTF8.GetString(tmpResult, 0, tmpResult.Length);
        }


    }
}

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
        public static byte[] SerializeSByte(SByte valueToSerialize)
        {
            // 1 or 2 bits for config (lsb)
            // x1 - stored in this byte (on rest 7 bits)
            // 00 - has default value 0
            // 10 - stored in separate byte

            // Case: 00 - default value
            if (valueToSerialize == 0)
                return new byte[1] { 0 };

            // Case 01 - value can be stored in configuration byte (on 7 bits => range 1-128 encoded as 0-127)
            if (valueToSerialize > 0 && valueToSerialize <= 127)
            {
                return new byte[1] { (byte)(0x01 | (byte)((valueToSerialize - 1)) << 1) };
            }

            // Case 10 - stored in separate byte
            List<byte> result = new List<byte>(); // Buffer for data
            
            // Encode value
            byte[] encodedValue = new byte[1] { (byte)valueToSerialize };

            // Prepare config byte
            byte config = (byte)0x02;

            // Store values in buffer
            result.Add(config);
            result.AddRange(encodedValue);

            // Return result
            return result.ToArray();
        }

        // Deserialization
        public static SByte DeserializeSByte(byte[] serializedData)
        {
            // 1 or 2 bits for config (lsb)
            // x1 - stored in this byte (on rest 7 bits)
            // 00 - has default value 0
            // 10 - stored in separate byte

            // Read config byte
            byte config = serializedData[0];

            // Is it value stored in config byte?
            if ((config & 0x01) > 0)
                return (SByte)((config >> 1) + 1);

            // Rest config cases
            byte configCase = (byte)(config & 0x03); // Mask 2 lsb bits

            switch (configCase)
            {
                case 2: // 10 - stored in separate byte
                    return (SByte)serializedData[1];

                // case 00:
                default:
                    return 0;
            }
        }
    }
}

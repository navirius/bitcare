﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Compression;
using System.IO;
using eSoft.Serializer.Infrastructure.Helpers;

namespace eSoft.Serializer
{
    // Int16 serialization
    public partial class Serializer
    {
        // Serialization of Int16
        public static byte[] SerializeUInt16(UInt16 valueToSerialize)
        {
            // 1 or 2 bits for config (lsb)
            // x1 - stored in this byte (on rest 7 bits)
            // 00 - has default value 0
            // 10 - stored in next few bytes - then next 2 bits says on how many bytes the value has been stored
            

            // Case: 00 - default value
            if (valueToSerialize == 0)
                return new byte[1] { 0 };

            // Case 1 - value can be stored in configuration byte (on 7 bits => range 1-128 encoded as 0-127)
            if (valueToSerialize>0 && valueToSerialize < 129)
            {
                return new byte[1] { (byte)(0x01 | (byte)((valueToSerialize - 1)) << 1) };
            }

            // Case 01 - stored in next few bytes - then next 2 bits says on how many bytes the value has been stored
            List<byte> result = new List<byte>(); // Buffer for data
            
            // Encode value
            byte[] encodedValue = BitToolkit.ConvertInt16ToByteArray((Int16)valueToSerialize);

            // Prepare config byte
            byte config = (byte)(0x02 | (byte)((encodedValue.Length + 1)) << 2);

            // Store values in buffer
            result.Add(config);
            result.AddRange(encodedValue);

            // Return result
            return result.ToArray();
        }

        // Deserialization of Int16
        public static UInt16 DeserializeUInt16(byte[] serializedData)
        {
            // 1 or 2 bits for config (lsb)
            // x1 - stored in this byte (on rest 7 bits)
            // 00 - has default value 0
            // 10 - stored in next few bytes - then next 2 bits says on how many bytes the value has been stored


            // Read config byte
            byte config = serializedData[0];

            // Is it value stored in config byte?
            if ((config & 0x01) > 0)
                return (UInt16)((config >> 1) + 1);

            // Rest config cases
            byte configCase = (byte)(config & 0x03); // Mask 2 lsb bits

            switch (configCase)
            {
                case 2: // 10 - stored in next few bytes - then next 2 bits says on how many bytes the value has been stored
                    return (UInt16)BitToolkit.ConvertByteArrayToInt16(serializedData,1);

                // case 00:
                default:
                    return 0;
            }
        }
    }
}

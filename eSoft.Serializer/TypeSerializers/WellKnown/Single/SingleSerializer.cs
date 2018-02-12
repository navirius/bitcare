using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown.Single_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class SingleSerializer : SerializedObject,IObjectSerializer<Single>
    {
        // Constructor
        public SingleSerializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(Single valueToSerialize)
        {
            // Is it default value
            if (valueToSerialize == 0)
            {
                SerializerStorage.WriteStorageFormat(new DefaultValue());
                return;
            }

            byte[] singleBytes=BitConverter.GetBytes(valueToSerialize);
            
            byte configByte = 0;
            byte[] tmpBytes = new byte[4]; // 4 bytes of Single
            int storedTmpBytes = 0;

            // Store bytes in buffer
            for (int pos = 0; pos < 4; pos++)
            {
                if (singleBytes[pos] > 0)
                {
                    configByte |= (byte)(1 << pos); // If byte is different then 0
                    tmpBytes[storedTmpBytes] = singleBytes[pos]; // Copy byte to output list
                    storedTmpBytes++;
                }
            }

            byte[] packedBytes = new byte[storedTmpBytes];
            Array.Copy(tmpBytes, 0, packedBytes, 0, storedTmpBytes);

            SerializerStorage.WriteStorageFormat(new ValueInDataStream(configByte));
            SerializerStorage.WritePackedData(packedBytes);
        }

        // Deserialization
        public Single Deserialize()
        {
            // Read info about storage format
            SingleStorageFormats format = (SingleStorageFormats)SerializerStorage.ReadStorageFormatId(SingleStorageBase.FormatIdSizeInBits);

            // Is it default value
            if (format == SingleStorageFormats.DefaultValue)
                return 0;

            // Deserialize full data

            // Read config byte - 4 bits for 4 bytes
            byte config = (byte) SerializerStorage.ReadStorageFormatData(4);
            
            // Single bytes
            byte[] singleBytes = new byte[4];

            // Conversion - we check out bits for all the bytes (4 bytes)
            for (int bitPos = 0; bitPos < 4; ++bitPos)
            {
                // If bit is set (has value 1) then we put this byte on proper position
                if ((config & (1 << bitPos)) > 0)
                {
                    singleBytes[bitPos] = SerializerStorage.ReadPackedDataByte();
                }
            }

            // Return result
            return BitConverter.ToSingle(singleBytes, 0);
        }
    }
}

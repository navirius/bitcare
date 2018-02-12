using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown.Int32_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class Int32Serializer : SerializedObject,IObjectSerializer<Int32>
    {
        // Constructor
        public Int32Serializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(Int32 valueToSerialize)
        {
            // Is it default value
            if (valueToSerialize == 0)
            {
                SerializerStorage.WriteStorageFormat(new DefaultValue());
                return;
            }

            // Should we store value in main data stream?
            if (valueToSerialize>0 && valueToSerialize <= ValueInConfig.MaxValueToStoreInConfig)
            {
                // We can store value in config
                SerializerStorage.WriteStorageFormat(new ValueInConfig(valueToSerialize));
            }
            else
            {
                byte[] packedData = BitToolkit.ConvertInt32ToByteArray(valueToSerialize);
                SerializerStorage.WriteStorageFormat(new ValueInDataStream((byte)packedData.Length));
                SerializerStorage.WritePackedData(packedData);
            }
        }

        // Deserialization
        public Int32 Deserialize()
        {
            // Read info about storage format
            Int32StorageFormats format = (Int32StorageFormats)SerializerStorage.ReadStorageFormatId(Int32StorageBase.FormatIdSizeInBits);

            // Is it default value
            if (format == Int32StorageFormats.DefaultValue)
                return 0;

            if (format == Int32StorageFormats.ValueInConfig)
            {
                ValueInConfig valInConfig = new ValueInConfig();
                valInConfig.FormatConfig.Bits = SerializerStorage.ReadStorageFormatData(ValueInConfig.UsedConfigBitsForValue);
                return valInConfig.Value;
            }

            // Value stored in PackedData
            ValueInDataStream valInDataStream = new ValueInDataStream();
            valInDataStream.FormatConfig.Bits = SerializerStorage.ReadStorageFormatData(ValueInDataStream.UsedConfigBitsForCase);
            byte[] encodedValue = SerializerStorage.ReadPackedData(valInDataStream.PackedDataSize);
            
            // Return decoded value
            return BitToolkit.ConvertByteArrayToInt32(encodedValue);
        }
    }
}

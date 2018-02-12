using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown.Int64_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class UInt64Serializer : SerializedObject,IObjectSerializer<UInt64>
    {
        // Constructor
        public UInt64Serializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(UInt64 valueToSerialize)
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
                SerializerStorage.WriteStorageFormat(new ValueInConfig((Int64)valueToSerialize));
            }
            else
            {
                byte[] packedData = BitToolkit.ConvertInt64ToByteArray((Int64)valueToSerialize);
                SerializerStorage.WriteStorageFormat(new ValueInDataStream((byte)packedData.Length));
                SerializerStorage.WritePackedData(packedData);
            }
        }

        // Deserialization
        public UInt64 Deserialize()
        {
            // Read info about storage format
            Int64StorageFormats format = (Int64StorageFormats)SerializerStorage.ReadStorageFormatId(Int64StorageBase.FormatIdSizeInBits);

            // Is it default value
            if (format == Int64StorageFormats.DefaultValue)
                return 0;

            if (format == Int64StorageFormats.ValueInConfig)
            {
                ValueInConfig valInConfig = new ValueInConfig();
                valInConfig.FormatConfig.Bits = SerializerStorage.ReadStorageFormatData(ValueInConfig.UsedConfigBitsForValue);
                return (UInt64)valInConfig.Value;
            }

            // Value stored in PackedData
            ValueInDataStream valInDataStream = new ValueInDataStream();
            valInDataStream.FormatConfig.Bits = SerializerStorage.ReadStorageFormatData(ValueInDataStream.UsedConfigBitsForCase);
            byte[] encodedValue = SerializerStorage.ReadPackedData(valInDataStream.PackedDataSize);
            
            // Return decoded value
            return (UInt64)BitToolkit.ConvertByteArrayToInt64(encodedValue);
        }
    }
}

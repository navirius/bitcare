using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown.Int32_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class UInt32Serializer : SerializedObject,IObjectSerializer<UInt32>
    {
        // Constructor
        public UInt32Serializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(UInt32 valueToSerialize)
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
                SerializerStorage.WriteStorageFormat(new ValueInConfig((Int32)valueToSerialize));
            }
            else
            {
                byte[] packedData = BitToolkit.ConvertInt32ToByteArray((Int32)valueToSerialize);
                SerializerStorage.WriteStorageFormat(new ValueInDataStream((byte)packedData.Length));
                SerializerStorage.WritePackedData(packedData);
            }
        }

        // Deserialization
        public UInt32 Deserialize()
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
                return (UInt32)valInConfig.Value;
            }

            // Value stored in PackedData
            ValueInDataStream valInDataStream = new ValueInDataStream();
            valInDataStream.FormatConfig.Bits = SerializerStorage.ReadStorageFormatData(ValueInDataStream.UsedConfigBitsForCase);
            byte[] encodedValue = SerializerStorage.ReadPackedData(valInDataStream.PackedDataSize);
            
            // Return decoded value
            return (UInt32)BitToolkit.ConvertByteArrayToInt32(encodedValue);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown.Int16_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class Int16Serializer : SerializedObject,IObjectSerializer<Int16>
    {
        // Constructor
        public Int16Serializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(Int16 valueToSerialize)
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
                byte[] packedData = BitToolkit.ConvertInt16ToByteArray(valueToSerialize);
                SerializerStorage.WriteStorageFormat(new ValueInDataStream((byte)packedData.Length));
                SerializerStorage.WritePackedData(packedData);
            }
        }

        // Deserialization
        public Int16 Deserialize()
        {
            // Read info about storage format
            Int16StorageFormats format = (Int16StorageFormats)SerializerStorage.ReadStorageFormatId(Int16StorageBase.FormatIdSizeInBits);

            // Is it default value
            if (format == Int16StorageFormats.DefaultValue)
                return 0;

            if (format == Int16StorageFormats.ValueInConfig)
            {
                ValueInConfig valInConfig = new ValueInConfig();
                valInConfig.FormatConfig.Bits = SerializerStorage.ReadStorageFormatData(ValueInConfig.UsedConfigBitsForValue);
                return (Int16)valInConfig.Value;
            }

            // Value stored in PackedData
            ValueInDataStream valInDataStream = new ValueInDataStream();
            valInDataStream.FormatConfig.Bits = SerializerStorage.ReadStorageFormatData(ValueInDataStream.UsedConfigBitsForCase);
            byte[] encodedValue = SerializerStorage.ReadPackedData(valInDataStream.PackedDataSize);
            
            // Return decoded value
            return BitToolkit.ConvertByteArrayToInt16(encodedValue);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown.Byte_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class SByteSerializer : SerializedObject,IObjectSerializer<SByte>
    {
        // Constructor
        public SByteSerializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(SByte valueToSerialize)
        {
            // Is it default value
            if (valueToSerialize == 0)
            {
                SerializerStorage.WriteStorageFormat(new DefaultValue());
                return;
            }

            // We can store value as separate byte
            SerializerStorage.WriteStorageFormat(new ValueInDataStream());
            SerializerStorage.WritePackedData(new byte[1] { (byte)valueToSerialize });
        }

        // Deserialization
        public SByte Deserialize()
        {
            // Read info about storage format
            ByteStorageFormats format = (ByteStorageFormats)SerializerStorage.ReadStorageFormatId(ByteStorageBase.FormatIdSizeInBits);

            // Is it default value
            if (format == ByteStorageFormats.DefaultValue)
                return 0;

            // Value stored in PackedData
            byte[] encodedValue = SerializerStorage.ReadPackedData(1);
            
            // Return decoded value
            return (SByte)encodedValue[0];
        }
    }
}

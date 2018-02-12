using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown.Guid_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class GuidSerializer : SerializedObject,IObjectSerializer<Guid>
    {
        // Constructor
        public GuidSerializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(Guid valueToSerialize)
        {
            // Is it default value
            if (valueToSerialize == Guid.Empty)
            {
                SerializerStorage.WriteStorageFormat(new DefaultValue());
                return;
            }

            SerializerStorage.WriteStorageFormat(new ValueInDataStream());

            byte[] guidBytes = BitToolkit.ConvertGuidToByteArray(valueToSerialize); // 16 bytes
            SerializerStorage.WritePackedData(guidBytes);
        }

        // Deserialization
        public Guid Deserialize()
        {
            // Read info about storage format
            GuidStorageFormats format = (GuidStorageFormats)SerializerStorage.ReadStorageFormatId(GuidStorageBase.FormatIdSizeInBits);

            // Is it default value
            if (format == GuidStorageFormats.DefaultValue)
                return Guid.Empty;

            // Deserialize full data
            byte[] guidBytes = SerializerStorage.ReadPackedData(16); // 16 bytes
            return BitToolkit.ConvertByteArrayToGuid(guidBytes);
        }
    }
}

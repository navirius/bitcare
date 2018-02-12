using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown.Double_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class DoubleSerializer : SerializedObject,IObjectSerializer<Double>
    {
        // Constructor
        public DoubleSerializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(Double valueToSerialize)
        {
            // Is it default value
            if (valueToSerialize == 0)
            {
                SerializerStorage.WriteStorageFormat(new DefaultValue());
                return;
            }

            SerializerStorage.WriteStorageFormat(new ValueInDataStream());
            new Int64Serializer(SerializerStorage).Serialize(BitConverter.DoubleToInt64Bits(valueToSerialize));
        }

        // Deserialization
        public Double Deserialize()
        {
            // Read info about storage format
            DoubleStorageFormats format = (DoubleStorageFormats)SerializerStorage.ReadStorageFormatId(DoubleStorageBase.FormatIdSizeInBits);

            // Is it default value
            if (format == DoubleStorageFormats.DefaultValue)
                return 0;

            // Deserialize full data
            return BitConverter.Int64BitsToDouble(new Int64Serializer(SerializerStorage).Deserialize());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown.Boolean_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class BooleanSerializer : SerializedObject, IObjectSerializer<Boolean>
    {
        // Constructor
        public BooleanSerializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(Boolean valueToSerialize)
        {
            // Is it True value
            if (valueToSerialize)
            {
                SerializerStorage.WriteStorageFormat(new TrueValue());
                return;
            }

            // False value
            SerializerStorage.WriteStorageFormat(new FalseValue());
        }

        // Deserialization
        public Boolean Deserialize()
        {
            // Read info about storage format
            BooleanStorageFormats format = (BooleanStorageFormats)SerializerStorage.ReadStorageFormatId(BooleanStorageBase.FormatIdSizeInBits);

            // Is it True value
            if (format == BooleanStorageFormats.TrueValue)
                return true;

            // It's False value
            return false;
        }
    }
}

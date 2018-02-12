using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown.Object_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class ObjectSerializer : SerializedObject, IObjectSerializer<Object>
    {
        // Constructor
        public ObjectSerializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(Object valueToSerialize)
        {
            if (valueToSerialize!=null)
            {
                SerializerStorage.WriteStorageFormat(new ObjectValue());
                return;
            }

            SerializerStorage.WriteStorageFormat(new NullValue());
        }

        // Deserialization
        public Object Deserialize()
        {
            // Read info about storage format
            ObjectStorageFormats format = (ObjectStorageFormats)SerializerStorage.ReadStorageFormatId(ObjectStorageBase.FormatIdSizeInBits);

            if (format == ObjectStorageFormats.ObjectValue)
                return new Object();

            return null;
        }
    }
}

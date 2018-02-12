using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class CharSerializer : SerializedObject,IObjectSerializer<Char>
    {
        // Constructor
        public CharSerializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(Char valueToSerialize)
        {
            new ByteSerializer(SerializerStorage).Serialize(Convert.ToByte(valueToSerialize));
        }

        // Deserialization
        public Char Deserialize()
        {
            return Convert.ToChar(new ByteSerializer(SerializerStorage).Deserialize());
        }
    }
}

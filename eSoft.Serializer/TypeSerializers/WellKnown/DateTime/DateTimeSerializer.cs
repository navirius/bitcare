// -----------------------------------------------------------------------
// <copyright file="DateTimeSerializer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown.Int32_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class DateTimeSerializer : SerializedObject, IObjectSerializer<DateTime>
    {
        // Constructor
        public DateTimeSerializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(DateTime valueToSerialize)
        {
            //Int64 value = valueToSerialize.ToBinary();
            Int64 value = valueToSerialize.Ticks;
            new Int64Serializer(SerializerStorage).Serialize(value);
        }

        // Deserialization
        public DateTime Deserialize()
        {
            Int64 value = new Int64Serializer(SerializerStorage).Deserialize();
            //return DateTime.FromBinary(value);
            return new DateTime(value);
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="DateTimeOffsetSerializer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class DateTimeOffsetSerializer : SerializedObject, IObjectSerializer<DateTimeOffset>
    {
        // Constructor
        public DateTimeOffsetSerializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(DateTimeOffset valueToSerialize)
        {
            new Int64Serializer(SerializerStorage).Serialize(valueToSerialize.Ticks);
        }

        // Deserialization
        public DateTimeOffset Deserialize()
        {
            return new DateTimeOffset(new Int64Serializer(SerializerStorage).Deserialize(),TimeSpan.Zero);
        }
    }
}

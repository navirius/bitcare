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

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class TimeSpanSerializer : SerializedObject, IObjectSerializer<TimeSpan>
    {
        // Constructor
        public TimeSpanSerializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(TimeSpan valueToSerialize)
        {
            new Int64Serializer(SerializerStorage).Serialize(valueToSerialize.Ticks);
        }

        // Deserialization
        public TimeSpan Deserialize()
        {
            return TimeSpan.FromTicks(new Int64Serializer(SerializerStorage).Deserialize());
        }
    }
}

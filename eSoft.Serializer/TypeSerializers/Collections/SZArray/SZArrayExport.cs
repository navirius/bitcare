using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Compression;
using System.IO;
using eSoft.Serializer.TypeSerializers.Factory;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown;

namespace eSoft.Serializer
{
    // Array serialization
    public partial class Serializer
    {
        // Serialization
        public static byte[] SerializeSZArray<T>(T[] valueToSerialize)
        {
            // main serializer
            Serializer serializer = new Serializer();

            // Serialization to data stores
            SZArraySerializer<T> arraySerializer = new SZArraySerializer<T>(serializer);
            arraySerializer.Serialize(valueToSerialize);

            // Pack the result to byte[]
            return serializer.ToByteArray();
        }

        // Deserialization
        public static T[] DeserializeSZArray<T>(byte[] serializedData)
        {
            // Main serializer
            Serializer serializer = new Serializer();

            // Restore store values
            serializer.InitStoresFromSerializedData(serializedData);

            // Deserialize from stores
            SZArraySerializer<T> arraySerializer = new SZArraySerializer<T>(serializer);
            return arraySerializer.Deserialize();
        }
    }
}

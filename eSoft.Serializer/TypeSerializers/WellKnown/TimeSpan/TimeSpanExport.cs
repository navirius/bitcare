using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Compression;
using System.IO;
using eSoft.Serializer.Infrastructure.Helpers;

namespace eSoft.Serializer
{
    public partial class Serializer
    {
        public static byte[] SerializeTimeSpan(TimeSpan valueToSerialize)
        {
            return Serializer.SerializeInt64(valueToSerialize.Ticks);
        }

        public static TimeSpan DeserializeTimeSpan(byte[] serializedData)
        {
            return TimeSpan.FromTicks(DeserializeInt64(serializedData));
        }
    }
}

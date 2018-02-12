using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.TypeSerializers.WellKnown.String_StorageFormats
{
    public class CachedString : StringStorageBase
    {
        // Size of string is stored as part of string PackedData
        public CachedString() : base(StringStorageFormats.CachedString, 0) { }
    }
}

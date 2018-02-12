using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.TypeSerializers.ComplexType_StorageFormats
{
    public class CachedObject : ComplexTypeStorageBase
    {
        // Size of string is stored as part of string PackedData
        public CachedObject() : base(ComplexTypeStorageFormats.CachedObject, 0) { }
    }
}

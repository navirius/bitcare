// -----------------------------------------------------------------------
// <copyright file="NullObject.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.TypeSerializers.ComplexType_StorageFormats
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NullObject : ComplexTypeStorageBase
    {
        // Null string doesn't need any additional config info
        public NullObject() : base(ComplexTypeStorageFormats.NullObject, 0) { }
    }
}

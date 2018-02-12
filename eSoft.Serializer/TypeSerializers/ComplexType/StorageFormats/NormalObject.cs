// -----------------------------------------------------------------------
// <copyright file="NormalObject.cs" company="">
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
    public class NormalObject : ComplexTypeStorageBase
    {
        // Size of string is stored as part of string PackedData
        public NormalObject() : base(ComplexTypeStorageFormats.NormalObject, 0) { }
    }
}

// -----------------------------------------------------------------------
// <copyright file="NormalArray.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.TypeSerializers.Collections.SZArray_StorageFormats
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NormalArray : ArrayStorageBase
    {
        // Size of Array is stored as part of Array PackedData
        public NormalArray() : base(ArrayStorageFormats.NormalArray, 0) { }
    }
}

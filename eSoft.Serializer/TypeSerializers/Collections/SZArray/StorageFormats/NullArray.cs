// -----------------------------------------------------------------------
// <copyright file="NullArray.cs" company="">
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
    public class NullArray : ArrayStorageBase
    {
        // Null Array doesn't need any additional config info
        public NullArray() : base(ArrayStorageFormats.NullArray, 0) { }
    }
}

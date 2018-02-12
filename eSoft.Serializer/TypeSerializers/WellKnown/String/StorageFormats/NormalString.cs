// -----------------------------------------------------------------------
// <copyright file="NormalString.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.TypeSerializers.WellKnown.String_StorageFormats
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NormalString : StringStorageBase
    {
        // Size of string is stored as part of string PackedData
        public NormalString() : base(StringStorageFormats.NormalString, 0) { }
    }
}

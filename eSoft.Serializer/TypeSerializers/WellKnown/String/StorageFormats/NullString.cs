// -----------------------------------------------------------------------
// <copyright file="NullString.cs" company="">
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
    public class NullString : StringStorageBase
    {
        // Null string doesn't need any additional config info
        public NullString() : base(StringStorageFormats.NullString, 0) { }
    }
}

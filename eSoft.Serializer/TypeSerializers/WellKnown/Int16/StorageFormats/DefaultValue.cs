// -----------------------------------------------------------------------
// <copyright file="DefaultValue.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.TypeSerializers.WellKnown.Int16_StorageFormats
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DefaultValue : Int16StorageBase
    {
        // We don't store value so we should store 2 bits in such case for default value
        public const int UsedConfigBitsForCase = 0;    

        // Constructor
        public DefaultValue() : base(Int16StorageFormats.DefaultValue, UsedConfigBitsForCase) { }
    }
}

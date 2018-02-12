// -----------------------------------------------------------------------
// <copyright file="ValueInDataStream.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.StorageFormat;

namespace eSoft.Serializer.TypeSerializers.WellKnown.Guid_StorageFormats
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ValueInDataStream : GuidStorageBase
    {
        public const int UsedConfigBitsForCase = 0;

        // Default constructor
        public ValueInDataStream() : base(GuidStorageFormats.ValueInDataStream, UsedConfigBitsForCase) { }
    }
}

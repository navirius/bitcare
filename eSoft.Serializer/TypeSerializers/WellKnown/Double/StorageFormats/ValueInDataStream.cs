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

namespace eSoft.Serializer.TypeSerializers.WellKnown.Double_StorageFormats
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ValueInDataStream : DoubleStorageBase
    {
        public const int UsedConfigBitsForCase = 0;

        // Default constructor
        public ValueInDataStream() : base(DoubleStorageFormats.ValueInDataStream, UsedConfigBitsForCase) { }
    }
}

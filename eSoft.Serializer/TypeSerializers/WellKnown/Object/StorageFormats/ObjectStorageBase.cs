// -----------------------------------------------------------------------
// <copyright file="Int32ConfigCaseBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.StorageFormat;

namespace eSoft.Serializer.TypeSerializers.WellKnown.Object_StorageFormats
{
    // Config cases
    public enum ObjectStorageFormats { NullValue, ObjectValue }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ObjectStorageBase : FormatBase, IStorageFormat
    {
        // Size of Id value in bits
        public const byte FormatIdSizeInBits = 1;

        // Constructor that requires config case value
        public ObjectStorageBase(ObjectStorageFormats confCase, byte usedConfigBits): base((byte)confCase, FormatIdSizeInBits, usedConfigBits) { }

        // Casted config case value
        public ObjectStorageFormats FormatType { get { return (ObjectStorageFormats)FormatId; } }
    }
}

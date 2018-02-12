// -----------------------------------------------------------------------
// <copyright file="GuidConfigCaseBase.cs" company="">
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
    // Config cases
    public enum GuidStorageFormats { DefaultValue, ValueInDataStream }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GuidStorageBase : FormatBase, IStorageFormat
    {
        // Size of Id value in bits
        public const byte FormatIdSizeInBits = 1;

        // Constructor that requires config case value
        public GuidStorageBase(GuidStorageFormats confCase, byte usedConfigBits): base((byte)confCase, FormatIdSizeInBits, usedConfigBits) { }

        // Casted config case value
        public GuidStorageFormats FormatType { get { return (GuidStorageFormats)FormatId; } }
    }
}

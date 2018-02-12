// -----------------------------------------------------------------------
// <copyright file="SingleConfigCaseBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.StorageFormat;

namespace eSoft.Serializer.TypeSerializers.WellKnown.Single_StorageFormats
{
    // Config cases
    public enum SingleStorageFormats { DefaultValue, ValueInDataStream }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SingleStorageBase : FormatBase, IStorageFormat
    {
        // Size of Id value in bits
        public const byte FormatIdSizeInBits = 1;

        // Constructor that requires config case value
        public SingleStorageBase(SingleStorageFormats confCase, byte usedConfigBits): base((byte)confCase, FormatIdSizeInBits, usedConfigBits) { }

        // Casted config case value
        public SingleStorageFormats FormatType { get { return (SingleStorageFormats)FormatId; } }
    }
}

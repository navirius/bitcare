// -----------------------------------------------------------------------
// <copyright file="ArrayStorageCaseBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.StorageFormat;

namespace eSoft.Serializer.TypeSerializers.Collections.SZArray_StorageFormats
{
    // Storage cases for Array
    public enum ArrayStorageFormats { NullArray, EmptyArray, NormalArray }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ArrayStorageBase : FormatBase, IStorageFormat
    {
        // Size of Id value in bits
        public const byte FormatIdSizeInBits = 2;

        // Constructor that requires config case value
        public ArrayStorageBase(ArrayStorageFormats confCase, byte usedConfigBits): base((byte)confCase, FormatIdSizeInBits, usedConfigBits) { }

        // Casted config case value
        public ArrayStorageFormats FormatType { get { return (ArrayStorageFormats)FormatId; } }
    }
}

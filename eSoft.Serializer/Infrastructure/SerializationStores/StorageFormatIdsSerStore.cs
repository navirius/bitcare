// -----------------------------------------------------------------------
// <copyright file="StorageCaseIdsBitConfArray.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.SerializationStores.Base;

namespace eSoft.Serializer.Infrastructure.SerializationStores
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class StorageFormatIdsSerStore : BinaryConfigArray
    {
        public byte ReadNextId(byte idSize)
        {
            return (byte)ReadNextBits(idSize);
        }
    }
}

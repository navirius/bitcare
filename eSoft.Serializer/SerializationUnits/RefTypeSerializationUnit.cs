// -----------------------------------------------------------------------
// <copyright file="RefTypeSerializationUnit.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.SerializationStores.Base;

namespace eSoft.Serializer.SerializationUnits
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RefTypeSerializationUnit : BaseSerializationUnit
    {
        // Constructor
        public RefTypeSerializationUnit()
        {
            ConfigArray = new BinaryConfigArray();
            StoredTypeId = 0; // 0 means it's base type (as opposite to derived type)
        }

        // Config Array
        public BinaryConfigArray ConfigArray;

        // We use TypeId only when we store derived type as a field value
        public int StoredTypeId;
    }
}

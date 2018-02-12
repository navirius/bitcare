// -----------------------------------------------------------------------
// <copyright file="ICachedObjectSerializer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.ObjectDictionaries.ValType;

namespace eSoft.Serializer.TypeSerializers
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICachedObjectSerializer<TObjectType>
    {
        // Serialization
        void Serialize(TObjectType valueToSerialize);

        // Deserialization
        TObjectType Deserialize();
    }
}

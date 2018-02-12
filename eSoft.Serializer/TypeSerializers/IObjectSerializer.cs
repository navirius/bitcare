// -----------------------------------------------------------------------
// <copyright file="IObjectSerializer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.TypeSerializers
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IObjectSerializer<T>
    {
        // Serialization
        void Serialize(T valueToSerialize);

        // Deserialization
        T Deserialize();
    }
}

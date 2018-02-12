// -----------------------------------------------------------------------
// <copyright file="ISerializedObject.cs" company="">
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
    public interface ISerializedObject
    {
        // Serialized type handle
        RuntimeTypeHandle TypeHandle { get; set; }
        ISerializerStorage SerializerStorage { get; set; }
    }
}

// -----------------------------------------------------------------------
// <copyright file="SerializedObject.cs" company="">
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
    public class SerializedObject : ISerializedObject
    {
        public RuntimeTypeHandle TypeHandle { get; set; }

        // Part of ISerializedObject
        // Constructor
        public SerializedObject(ISerializerStorage serializerStorage) { SerializerStorage = serializerStorage; }

        // Members
        public ISerializerStorage SerializerStorage { get; set; }
    }
}

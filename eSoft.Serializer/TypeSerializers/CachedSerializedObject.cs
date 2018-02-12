// -----------------------------------------------------------------------
// <copyright file="CachedSerializedObject.cs" company="">
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
    public class CachedSerializedObject<TDictType> : SerializedObject, ICachedSerializedObject
    {
        // Part of ICachedSerializedObject
        public bool IsRegisteredAlready { get; set; }
        public int ObjectId { get; set; }

        // Part of ICachedObjectSerializer
        // Constructor
        public CachedSerializedObject(ISerializerStorage serializerStorage, TDictType objectCache) : base(serializerStorage) { ObjectCache = objectCache; }

        // Members
        public TDictType ObjectCache { get; set; }
    }
}

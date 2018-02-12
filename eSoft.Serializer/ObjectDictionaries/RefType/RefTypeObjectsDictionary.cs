// -----------------------------------------------------------------------
// <copyright file="ProcessedRefObjectsDictionary.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using eSoft.Serializer.TypeSerializers;
using System.Runtime.CompilerServices;
using eSoft.Serializer.ObjectDictionaries.ValType;
using eSoft.Serializer.Exceptions;

namespace eSoft.Serializer.ObjectDictionaries.RefType
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RefTypeObjectsDictionary : IRefTypeObjectsDictionary
    {
        // Dictionary with indexes (IDs) of stored objects (ByRef objects)
        // Key is unique ID (identity) of processed object, Value is internal serializer ID of processed object
        private Dictionary<int, int> m_ObjectsDict;

        // Key is internal serializer ID of processed object, Value is objects value
        private Dictionary<int, object> m_IdsDict;

        // Counter we use to generate/assign internal unique Ids on stored objects
        private int m_ObjectId;

        public RefTypeObjectsDictionary()
        {
            m_ObjectsDict = new Dictionary<int, int>();
            m_IdsDict = new Dictionary<int, object>();
            m_ObjectId = 0;
        }

        // Gets next available object ID for ProcessedObjects
        private int GetNextObjectId()
        {
            //return Interlocked.Increment(ref m_ObjectId);
            return ++m_ObjectId;
        }

        // It returns true if object full data should be stored instead of cached Id
        public bool GetObjectIdForRefTypeField<T4PO>(T4PO processedObject, ICachedSerializedObject objectSerializer) 
        {
            // Take system wide unique id of the object (unique at the moment of serialization)
            int objectKey = RuntimeHelpers.GetHashCode(processedObject);

            // Do we know this object already
            if (m_ObjectsDict.ContainsKey(objectKey))
            {
                objectSerializer.IsRegisteredAlready = true;
                objectSerializer.ObjectId = m_ObjectsDict[objectKey];

                // We should persist registered object Id in such case only
                return false;
            }

            // Info that new Id has been allocated and object should be persisted instead of using ID only
            objectSerializer.IsRegisteredAlready = false;

            // Next object id
            objectSerializer.ObjectId = GetNextObjectId();

            // Store in value cache
            m_ObjectsDict.Add(objectKey, objectSerializer.ObjectId);

            // Should be persisted
            return true;
        }

        // For deserialization
        public T4PO GetObjectValueForRefTypeField<T4PO>(ICachedSerializedObject valueSerializer)
        {
            if (!m_IdsDict.ContainsKey(valueSerializer.ObjectId))
                throw new SerializerException("Value Type Dictionary for provided type doesn't exist");

            return (T4PO)m_IdsDict[valueSerializer.ObjectId];
        }

        // For deserialization
        public void RegisterValue<T4PO>(T4PO processedObject, ICachedSerializedObject valueSerializer)
        {
            // Is it registered already?
            if (m_IdsDict.ContainsKey(valueSerializer.ObjectId))
                return;

            // Register in helper collection
            m_IdsDict.Add(valueSerializer.ObjectId, valueSerializer);

            // Take system wide unique id of the object (unique at the moment of operation)
            int objectKey = RuntimeHelpers.GetHashCode(processedObject);

            // Store in value cache
            m_ObjectsDict.Add(objectKey, valueSerializer.ObjectId);
        }

        public void Reset()
        {
            m_ObjectsDict.Clear();
            m_IdsDict.Clear();
            m_ObjectId = 0;
        }
    }
}

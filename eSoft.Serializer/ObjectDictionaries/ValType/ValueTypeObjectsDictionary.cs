// -----------------------------------------------------------------------
// <copyright file="ValueTypeObjectsDictionary.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using eSoft.Serializer.TypeSerializers;
using eSoft.Serializer.Exceptions;

namespace eSoft.Serializer.ObjectDictionaries.ValType
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ValueTypeObjectsDictionary : IValueTypeObjectsDictionary
    {
        // Key is runtime type handle, value is Dictionary with ValueType values
        private Dictionary<RuntimeTypeHandle, object> m_ValueTypeDictionaries;

        // Default constructor
        public ValueTypeObjectsDictionary()
        {
            m_ValueTypeDictionaries = new Dictionary<RuntimeTypeHandle, object>();
        }

        // It returns true if object full data should be stored instead of cached Id
        public bool GetObjectIdForValueTypeField<T4PO>(T4PO processedObject, ICachedSerializedObject valueSerializer)
        {
            // Check if we have such value in dictionary for specific type
            ValTypeDictEntry<T4PO> valTypeDictEntry = null;

            // Get existing dictionary with registered values for provided type
            if (m_ValueTypeDictionaries.ContainsKey(valueSerializer.TypeHandle))
                valTypeDictEntry = (ValTypeDictEntry<T4PO>)m_ValueTypeDictionaries[valueSerializer.TypeHandle];

            // If such dictionary deosn't exist yet - create a new one
            if (valTypeDictEntry == null)
            {
                valTypeDictEntry = new ValTypeDictEntry<T4PO>();
                m_ValueTypeDictionaries.Add(valueSerializer.TypeHandle, valTypeDictEntry);
            }
             
            // If we have such value then we feel proper members
            if (valTypeDictEntry.ValDict.ContainsKey(processedObject))
            {
                valueSerializer.IsRegisteredAlready = true;
                valueSerializer.ObjectId = valTypeDictEntry.ValDict[processedObject];
                return false; // Value from cache - system should not persist original data
            }

            // It's unknown value yet - so let's register it in dict
            valueSerializer.IsRegisteredAlready = false;
            valueSerializer.ObjectId = valTypeDictEntry.GetNextObjectId();
            valTypeDictEntry.ValDict.Add(processedObject, valueSerializer.ObjectId);
            
            // Should be persisted
            return true;
        }

        // It returns true if object full data should be stored instead of cached Id
        public T4PO GetObjectValueForValueTypeField<T4PO>(ICachedSerializedObject valueSerializer)
        {
            // Check if we have such value in dictionary for specific type
            ValTypeDictEntry<T4PO> valTypeDictEntry = null;

            // Get existing dictionary with registered values for provided type
            if (m_ValueTypeDictionaries.ContainsKey(valueSerializer.TypeHandle))
                valTypeDictEntry = (ValTypeDictEntry<T4PO>)m_ValueTypeDictionaries[valueSerializer.TypeHandle];
            else
                throw new SerializerException("Value Type Dictionary for provided type doesn't exist");

            return valTypeDictEntry.IdDict[valueSerializer.ObjectId];
        }

        public void RegisterValue<T4PO>(T4PO processedObject, ICachedSerializedObject valueSerializer)
        {
            // Check if we have such value in dictionary for specific type
            ValTypeDictEntry<T4PO> valTypeDictEntry = null;

            // Get existing dictionary with registered values for provided type
            if (m_ValueTypeDictionaries.ContainsKey(valueSerializer.TypeHandle))
                valTypeDictEntry = (ValTypeDictEntry<T4PO>)m_ValueTypeDictionaries[valueSerializer.TypeHandle];

            // If such dictionary deosn't exist yet - create a new one
            if (valTypeDictEntry == null)
            {
                valTypeDictEntry = new ValTypeDictEntry<T4PO>();
                m_ValueTypeDictionaries.Add(valueSerializer.TypeHandle, valTypeDictEntry);
            }

            // Register value in dict
            valTypeDictEntry.ValDict.Add(processedObject, valueSerializer.ObjectId);
            valTypeDictEntry.IdDict.Add(valueSerializer.ObjectId, processedObject);
        }

        public void Reset()
        {
            m_ValueTypeDictionaries.Clear();
        }
    }
}

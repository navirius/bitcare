// -----------------------------------------------------------------------
// <copyright file="ArraySerializer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.ObjectDictionaries.RefType;
using eSoft.Serializer.TypeSerializers.Collections.SZArray_StorageFormats;
using eSoft.Serializer.TypeSerializers.Factory;
using eSoft.Serializer.ObjectDictionaries.ValType;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SZRefArraySerializer<T> : SerializedObject
    {
        private Action<T> m_ElemSerializationAction;
        private Func<T> m_ElemDeserializationFunc;

        // Constructor
        public SZRefArraySerializer(ISerializerStorage serializerStorage, Action<T> elemSerializationAction, Func<T> elemDeserializationFunc)
            : base(serializerStorage) 
        {
            m_ElemSerializationAction = elemSerializationAction;
            m_ElemDeserializationFunc = elemDeserializationFunc;
        }

        // Serialization of simple elements, that doesn't use caching
        public void Serialize(T[] valueToSerialize)
        {
            // Case: null value
            if (valueToSerialize == null)
            {
                SerializerStorage.WriteStorageFormat(new NullArray());
                return;
            }

            // Case: empty Array
            if (valueToSerialize.Length == 0)
            {
                SerializerStorage.WriteStorageFormat(new EmptyArray());
                return;
            }

            // Case: normal Array with Id
            SerializerStorage.WriteStorageFormat(new NormalArray());

            // Store array size
            Int32Serializer intSerializer = new Int32Serializer(SerializerStorage);
            intSerializer.Serialize(valueToSerialize.Length);

            // Serialization of array elements
            for (int pos = 0; pos < valueToSerialize.Length; pos++)
                m_ElemSerializationAction(valueToSerialize[pos]);
        }

        // Generic deserialization
        public T[] Deserialize()
        {
            // Read info about storage format
            ArrayStorageFormats format = (ArrayStorageFormats)SerializerStorage.ReadStorageFormatId(ArrayStorageBase.FormatIdSizeInBits);

            // Case: null Array
            if (format == ArrayStorageFormats.NullArray)
                return null;

            // Case: empty Array
            if (format == ArrayStorageFormats.EmptyArray)
                return new T[0];

            // Restore array size
            Int32Serializer intSerializer = new Int32Serializer(SerializerStorage);
            Int32 arrayLength = intSerializer.Deserialize();

            // Case: normal Array
            T[] outputArray = new T[arrayLength];

            // Deserialize all the elems
            for (int pos = 0; pos < arrayLength; pos++)
            {
                T value = m_ElemDeserializationFunc();
                outputArray[pos] = value;
            }

            // Return result
            return outputArray;
        }
    }
}

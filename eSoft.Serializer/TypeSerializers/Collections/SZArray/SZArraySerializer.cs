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
    public class SZArraySerializer<T> : SerializedObject
    {
        // Constructor
        public SZArraySerializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization of simple elements, that doesn't use caching
        private void SerializeInternal(T[] valueToSerialize, Action arraySerializeAction)
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
            arraySerializeAction();
        }

        // Serialization of simple elements, that doesn't use caching
        private void SerializeInternal(T[] valueToSerialize, BaseSerializerInfo<T> serializerInfo)
        {
            Action arraySerializationAction=()=>
                {
                    // Serializer of an element
                    IObjectSerializer<T> serializer = (serializerInfo as NormalObjectSerializerInfo<T>).FactoryFunc(SerializerStorage);

                    // Serialize all the elems
                    for (int pos = 0; pos < valueToSerialize.Length; pos++)
                        serializer.Serialize(valueToSerialize[pos]);
                };

            SerializeInternal(valueToSerialize, arraySerializationAction);
        }

        // Serialization of value type elements that uses value type caching (string and so on)
        private void SerializeInternal(T[] valueToSerialize, BaseSerializerInfo<T> serializerInfo, IValueTypeObjectsDictionary objectCache)
        {
            Action arraySerializationAction = () =>
            {
                // Serializer of an element
                ICachedObjectSerializer<T> serializer = (serializerInfo as CachedValObjectSerializerInfo<T>).FactoryFunc(SerializerStorage, objectCache);

                // Serialize all the elems
                for (int pos = 0; pos < valueToSerialize.Length; pos++)
                    serializer.Serialize(valueToSerialize[pos]);
            };

            SerializeInternal(valueToSerialize, arraySerializationAction);
        }

        // Serialization of ref type elements that uses ref type caching (string and so on)
        private void SerializeInternal(T[] valueToSerialize, BaseSerializerInfo<T> serializerInfo, IRefTypeObjectsDictionary objectCache)
        {
            Action arraySerializationAction = () =>
            {
                // Serializer of an element
                ICachedObjectSerializer<T> serializer = (serializerInfo as CachedRefObjectSerializerInfo<T>).FactoryFunc(SerializerStorage, objectCache);

                // Serialize all the elems
                for (int pos = 0; pos < valueToSerialize.Length; pos++)
                    serializer.Serialize(valueToSerialize[pos]);
            };

            SerializeInternal(valueToSerialize, arraySerializationAction);
        }

        // Serialization of simple elements, that doesn't use caching
        public void Serialize(T[] valueToSerialize)
        {
            // Serializer info for the element
            BaseSerializerInfo<T> serializerInfo = (WKTSerializationFactory.GetSerializersForType(typeof(T).TypeHandle) as BaseSerializerInfo<T>);

            switch (serializerInfo.SerializerKind)
            {
                case SerializerKind.Normal:
                    SerializeInternal(valueToSerialize, serializerInfo);
                        return;
                    
                case SerializerKind.CachedVal:
                        SerializeInternal(valueToSerialize, serializerInfo, (this.SerializerStorage as Serializer).ValObjectsCache);
                        return;

                case SerializerKind.CachedRef:
                        SerializeInternal(valueToSerialize, serializerInfo, (this.SerializerStorage as Serializer).RefObjectsCache);
                        return;
            }
        }

        // Generic deserialization
        private T[] DeserializeInternal(Action<int, T[]> arrayDeserializationAction)
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

            // Deserialize array elements
            arrayDeserializationAction(arrayLength, outputArray);

            // Return result
            return outputArray;
        }

        // Deserialization of simple elements, that doesn't use caching
        private T[] DeserializeInternal(BaseSerializerInfo<T> serializerInfo)
        {
            Action<int, T[]> action = (arrayLength, outputArray) =>
                {
                    // Serializer of an element
                    IObjectSerializer<T> serializer = (serializerInfo as NormalObjectSerializerInfo<T>).FactoryFunc(SerializerStorage);

                    // Deserialize all the elems
                    for (int pos = 0; pos < arrayLength; pos++)
                    {
                        T value = serializer.Deserialize();
                        outputArray[pos] = value;
                    }
                };

            return DeserializeInternal(action);
        }

        // Deserialization of value type elements that uses value type caching (string and so on)
        private T[] DeserializeInternal(BaseSerializerInfo<T> serializerInfo, IValueTypeObjectsDictionary objectCache)
        {
            Action<int, T[]> action = (arrayLength, outputArray) =>
            {
                // Serializer of an element
                ICachedObjectSerializer<T> serializer = (serializerInfo as CachedValObjectSerializerInfo<T>).FactoryFunc(SerializerStorage, objectCache);

                // Deserialize all the elems
                for (int pos = 0; pos < arrayLength; pos++)
                {
                    T value = serializer.Deserialize();
                    outputArray[pos] = value;
                }
            };

            return DeserializeInternal(action);
        }

        // Serialization of ref type elements that uses ref type caching (string and so on)
        private T[] DeserializeInternal(BaseSerializerInfo<T> serializerInfo, IRefTypeObjectsDictionary objectCache)
        {
            Action<int, T[]> action = (arrayLength, outputArray) =>
            {
                // Serializer of an element
                ICachedObjectSerializer<T> serializer = (serializerInfo as CachedRefObjectSerializerInfo<T>).FactoryFunc(SerializerStorage, objectCache);

                // Deserialize all the elems
                for (int pos = 0; pos < arrayLength; pos++)
                {
                    T value = serializer.Deserialize();
                    outputArray[pos] = value;
                }
            };

            return DeserializeInternal(action);
        }

         public T[] Deserialize()
        {
            // Serializer info for the element
            BaseSerializerInfo<T> serializerInfo = (WKTSerializationFactory.GetSerializersForType(typeof(T).TypeHandle) as BaseSerializerInfo<T>);

            switch (serializerInfo.SerializerKind)
            {
                case SerializerKind.Normal:
                    return DeserializeInternal(serializerInfo);

                case SerializerKind.CachedVal:
                    return DeserializeInternal(serializerInfo, (this.SerializerStorage as Serializer).ValObjectsCache);

                case SerializerKind.CachedRef:
                    return DeserializeInternal(serializerInfo, (this.SerializerStorage as Serializer).RefObjectsCache);
            }

            return null;
        }
    }
}

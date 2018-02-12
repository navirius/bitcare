// -----------------------------------------------------------------------
// <copyright file="StringSerializer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.ObjectDictionaries.ValType;
using eSoft.Serializer.TypeSerializers.WellKnown.String_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class StringSerializer : CachedSerializedObject<IValueTypeObjectsDictionary>, ICachedObjectSerializer<string>
    {
        // Constructor
        public StringSerializer(ISerializerStorage serializerStorage, IValueTypeObjectsDictionary objectCache) : base(serializerStorage, objectCache) { }

        // Serialization
        public void Serialize(string valueToSerialize)
        {
            // Case: null value
            if (valueToSerialize == null)
            {
                SerializerStorage.WriteStorageFormat(new NullString());
                return;
            }

            // Case: empty string
            if(valueToSerialize==String.Empty)
            {
                SerializerStorage.WriteStorageFormat(new EmptyString());
                return;
            }

            // Int32 Serializer
            Int32Serializer int32Serializer = new Int32Serializer(SerializerStorage);

            // If caching has been activated
            if (SerializerStorage.UseValCaching)
            {
                // Regular string - we should obtain string Id from cache
                bool shouldStoreFullData = ObjectCache.GetObjectIdForValueTypeField(valueToSerialize, this);

                // If we should store full data
                if (shouldStoreFullData)
                {
                    // Case: normal string with Id
                    SerializerStorage.WriteStorageFormat(new NormalString());

                    // Store Id of string
                    int32Serializer.Serialize(this.ObjectId);

                    // Encode string to utf-8
                    byte[] stringData = Encoding.UTF8.GetBytes(valueToSerialize);

                    // Store string length
                    int32Serializer.Serialize(stringData.Length);

                    // Store string data
                    SerializerStorage.WritePackedData(stringData);
                }
                else
                {
                    // Case: cached string
                    SerializerStorage.WriteStorageFormat(new CachedString());

                    // Store Id of string
                    int32Serializer.Serialize(this.ObjectId);
                }
            }
            else
            {
                // Store value without caching
                SerializerStorage.WriteStorageFormat(new NormalString());

                // Encode string to utf-8
                byte[] stringData = Encoding.UTF8.GetBytes(valueToSerialize);

                // Store string length
                int32Serializer.Serialize(stringData.Length);

                // Store string data
                SerializerStorage.WritePackedData(stringData);
            }
        }

        // Deserialization
        public string Deserialize()
        {
            // Read info about storage format
            StringStorageFormats format = (StringStorageFormats)SerializerStorage.ReadStorageFormatId(StringStorageBase.FormatIdSizeInBits);

            // Case: null string
            if(format==StringStorageFormats.NullString)
                return null;

            // Case: empty string
            if(format==StringStorageFormats.EmptyString)
                return string.Empty;

            // Int32 serializer
            Int32Serializer int32Serializer = new Int32Serializer(SerializerStorage);

            // If caching has been activated
            if (SerializerStorage.UseValCaching)
            {
                // Case: cached string
                if (format == StringStorageFormats.CachedString)
                {
                    // Read object Id
                    this.ObjectId = int32Serializer.Deserialize();

                    // Take value from cache (objects dictionary)
                    return ObjectCache.GetObjectValueForValueTypeField<String>(this);
                }

                // Case: new string
                if (format == StringStorageFormats.NormalString)
                {
                    this.ObjectId = int32Serializer.Deserialize();// Read object Id
                    int encodedStringLength = int32Serializer.Deserialize(); // Read encoded string length
                    byte[] encodedString = SerializerStorage.ReadPackedData(encodedStringLength); // Read encoded data

                    // Decode string
                    string result = Encoding.UTF8.GetString(encodedString, 0, encodedString.Length);

                    // Register new string in cache
                    ObjectCache.RegisterValue(result, this);

                    // Return result
                    return result;
                }
            }
            else
            {
                // Value without caching (so without Id)
                int encodedStringLength = int32Serializer.Deserialize(); // Read encoded string length
                byte[] encodedString = SerializerStorage.ReadPackedData(encodedStringLength); // Read encoded data

                // Decode string
                return Encoding.UTF8.GetString(encodedString,0,encodedString.Length);
            }

            // Default result
            return null;
        }
    }
}

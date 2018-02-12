// -----------------------------------------------------------------------
// <copyright file="ObjectSerializerBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using eSoft.Serializer.ObjectDictionaries.RefType;
using eSoft.Serializer.ObjectDictionaries.ValType;
using eSoft.Serializer.TypeSerializers;
using eSoft.Serializer.TypeSerializers.ComplexType_StorageFormats;

namespace eSoft.Serializer
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class ComplexTypeSerializerBase : CachedSerializedObject<IRefTypeObjectsDictionary>
    {
        // Cache for value type objects
        public IValueTypeObjectsDictionary ValObjectsCache { get; set; }

        // Well Known Type Instance Serializers
        public WKTSerializers WKTSerializers;

        // Constructor
        public ComplexTypeSerializerBase(ISerializerStorage serializerStorage, IRefTypeObjectsDictionary refObjectCache, IValueTypeObjectsDictionary valObjectsCache)
            : base(serializerStorage, refObjectCache) { ValObjectsCache = valObjectsCache; WKTSerializers = serializerStorage.WKTSerializers; }

        // Serialize internal TypeId for polimorphic fields (that can store derived types)
        public void SerializePolimorphicFieldType(int typeId)
        {
            WKTSerializers.Int32.Serialize(typeId);
        }

        // Deserialize internal TypeId for polimorphic fields (that can store derived types)
        public Int32 DeserializePolimorphicFieldType()
        {
            return WKTSerializers.Int32.Deserialize();
        }

        public bool ShouldStoreFullData<T>(T valueToSerialize)
        {
            // Case: null value
            if (valueToSerialize == null)
            {
                SerializerStorage.WriteStorageFormat(new NullObject());
                return false;
            }

            // If caching has been activated
            if (SerializerStorage.UseRefCaching)
            {
                // Regular string - we should obtain string Id from cache
                bool shouldStoreFullData = this.ObjectCache.GetObjectIdForRefTypeField(valueToSerialize, this);

                // If we should store full data
                if (shouldStoreFullData)
                    SerializerStorage.WriteStorageFormat(new NormalObject()); // Case: normal object with Id
                else
                    SerializerStorage.WriteStorageFormat(new CachedObject()); // Case: cached object

                // Object Id
                WKTSerializers.Int32.Serialize(this.ObjectId);
            }
            else
            {
                // Store value without caching
                SerializerStorage.WriteStorageFormat(new NormalObject());
            }

            // Should store full data...
            return true;
        }

        public bool ShouldLoadFullData<T>(ref T objectValue) where T:class
        {
            // Read info about storage format
            ComplexTypeStorageFormats format = (ComplexTypeStorageFormats)SerializerStorage.ReadStorageFormatId(ComplexTypeStorageBase.FormatIdSizeInBits);

            // Case: null object
            if (format == ComplexTypeStorageFormats.NullObject)
            {
                objectValue = null;
                return false;
            }

            // If caching has been activated
            if (SerializerStorage.UseRefCaching)
            {
                // Deserialize object ID
                this.ObjectId = WKTSerializers.Int32.Deserialize();

                // Case: cached object
                if (format == ComplexTypeStorageFormats.CachedObject)
                {
                    objectValue = ObjectCache.GetObjectValueForRefTypeField<T>(this);
                    return false;
                }
            }

            // Case: normal object - load full data
            return true;
        }

        public void UpdateRefObjectsCache<T>(T objectValue) where T : class
        {
            // If we don't cache data then stop here...
            if (!SerializerStorage.UseRefCaching)
                return;

            // Register value in cache
            ObjectCache.RegisterValue(objectValue, this);
        }
    }
}

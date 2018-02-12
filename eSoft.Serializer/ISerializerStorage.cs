// -----------------------------------------------------------------------
// <copyright file="ISerializationStorage.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using eSoft.Serializer.Infrastructure.SerializationStores;
using eSoft.Serializer.Infrastructure.StorageFormat;

namespace eSoft.Serializer
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISerializerStorage
    {
        // Information about types of storage cases only
        StorageFormatIdsSerStore StorageFormatIds { get; set; }

        // Field Storage Configuration Map. It's bit array with info about members storage (configuration bits)
        StorageFormatDataSerStore StorageFormatData { get; set; }

        // Main data storage. If member value has not been stored in StorageCases map (because it can't be modified for instance), then it's stored here
        PackedDataSerStore PackedData { get; set; }

        // Switch to use caching at all
        bool UseValCaching { get; set; }
        bool UseRefCaching { get; set; }

        // Well Known Type Instance Serializers
        WKTSerializers WKTSerializers { get; set; }

        // Object ID
        //uint GetValueTypeObjectId<T>(T valType) where T : struct;

        // Store info about storage format (Id and Data)
        void WriteStorageFormat(IStorageFormat iStorageFormat);

        // Store optional PackedData
        void WritePackedData(byte[] packedData);

        // Read stored format Id
        byte ReadStorageFormatId(byte idSize);

        // Read storage config data
        uint ReadStorageFormatData(byte usedBits);

        // Read bytes from PackedData store
        byte[] ReadPackedData(int packedDataSize);

        // Read one byte from PackedData store
        byte ReadPackedDataByte();
    }
}

// -----------------------------------------------------------------------
// <copyright file="eSoftSerializer.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO;
using eSoft.Serializer.Compression;
using eSoft.Serializer.SerializationUnits;
using eSoft.Serializer.Infrastructure.SerializationStores;
using eSoft.Serializer.ObjectDictionaries.RefType;
using eSoft.Serializer.ObjectDictionaries.ValType;
using eSoft.Serializer.Infrastructure.StorageFormat;
using eSoft.Serializer.Infrastructure.Helpers;

namespace eSoft.Serializer
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class Serializer : ISerializerStorage
    {
        // We use 7 lowest bits to store info about serializer format version. MSB bit (b7) is used to signal, if the content has been compressed
        // Then next byte can contain Id of the compression tool used to compress data
        private const byte m_SerializerFormatVersion = 1;

        public IRefTypeObjectsDictionary RefObjectsCache { get; set; }
        public IValueTypeObjectsDictionary ValObjectsCache { get; set; }

        public bool UseValCaching { get; set; }
        public bool UseRefCaching { get; set; }

        public bool UseCaching { set { UseValCaching = value; UseRefCaching = value; } }

        // Compression object
        public ICompressionFactory CompressionFactory { get; set; }

        // Preferred compression type that should be used during compression
        public CompressionType ActiveCompressionType { get; set; }

        // Preferred compression level
        public int ActiveCompressionLevel { get; set; }

        // When we start using compression (it's size of data stream, when we have more bytes then threshold value we compress the stream)
        public int CompressionThreshold { get; set; }

        public StorageFormatIdsSerStore     StorageFormatIds    { get; set; } // Format Ids (storage case Id)
        public StorageFormatDataSerStore    StorageFormatData   { get; set; } // Storage configuration bits
        public PackedDataSerStore           PackedData          { get; set; } // Main data storage

        // Instance serializers for well known types
        public WKTSerializers WKTSerializers { get; set; }

        // Constructor
        public Serializer()
        {
            RefObjectsCache = new RefTypeObjectsDictionary();
            ValObjectsCache = new ValueTypeObjectsDictionary();

            // Compression factory
            CompressionFactory = new CompressionFactory();
            ActiveCompressionType = CompressionType.Internal;
            ActiveCompressionLevel = 1; // Fast compression

            // Above this value we start compressing resulting stream
            CompressionThreshold = 100;

            StorageFormatIds = new StorageFormatIdsSerStore();
            StorageFormatData = new StorageFormatDataSerStore();
            PackedData = new PackedDataSerStore();

            // Turn off caching by default
            UseCaching = false;

            // Instance serializers
            WKTSerializers= new WKTSerializers(SerializerStorage, ValObjectsCache);
        }

        // Construction from byte array (from serialized data)
        public void InitStoresFromSerializedData(byte[] serializedData)
        {
            int nextReadPos = 0; // Position of next read operation
            int readBytes = 0;

            // Read config
            SerializerConfiguration serConfig = SerializerConfiguration.FromByteArray(serializedData, out readBytes);

            // Update read pos
            nextReadPos += readBytes;

            // Assign original buffer
            byte[] dataBytes = serializedData;

            // Decompress when necessary
            if (serConfig.IsCompressed)
            {
                // All data bytes
                List<byte> compressedBytes = new List<byte>(serializedData.Skip(readBytes));
                dataBytes = new List<byte>(CompressionFactory.GetCompressionEngine(serConfig.CompressionType).Decompress(compressedBytes.ToArray())).ToArray();
                nextReadPos = 0;
            }

            // Restore StorageFormatIds bytes
            int storageSize = BitToolkit.DecodeSize(dataBytes, out readBytes, nextReadPos);
            nextReadPos += readBytes;

            StorageFormatIds.InitFromSerializedData(dataBytes.Skip(nextReadPos).Take(storageSize));
            nextReadPos += storageSize;

            // Restore StorageFormatData bytes
            storageSize = BitToolkit.DecodeSize(dataBytes, out readBytes, nextReadPos);
            nextReadPos += readBytes;

            StorageFormatData.InitFromSerializedData(dataBytes.Skip(nextReadPos).Take(storageSize));
            nextReadPos += storageSize;

            // Restore PackedData bytes
            storageSize = BitToolkit.DecodeSize(dataBytes, out readBytes, nextReadPos);
            nextReadPos += readBytes;

            PackedData.InitFromSerializedData(dataBytes.Skip(nextReadPos).Take(storageSize));
            nextReadPos += storageSize;

            // Update read positions
            PackedData.CachedValPosInStore = 0;
            StorageFormatIds.CachedValPosInStore = 0;
            StorageFormatData.CachedValPosInStore = 0;
        }

        // Serialization of internal data to byte array (Final step of serialization)
        public byte[] ToByteArray()
        {
            int StorageFormatIds_ByteArraySize = StorageFormatIds.GetByteArraySize();
            int StorageFormatData_ByteArraySize = StorageFormatData.GetByteArraySize();
            int PackedData_ByteArraySize = PackedData.ByteArraySize;

            byte[] storageFormatIdsSizeBytes = BitToolkit.EncodeSize(StorageFormatIds_ByteArraySize);
            byte[] storageFormatDataSizeBytes = BitToolkit.EncodeSize(StorageFormatData_ByteArraySize);
            byte[] packedDataSizeBytes = BitToolkit.EncodeSize(PackedData_ByteArraySize);

            // Storage estimated size
            int estimatedSize = StorageFormatIds_ByteArraySize + StorageFormatData_ByteArraySize + PackedData_ByteArraySize;

            // Serializer configuration
            SerializerConfiguration serConfig = new SerializerConfiguration();
            serConfig.SerializerFormatVersion = m_SerializerFormatVersion;

            // Compress if we have necessary data already
            if (ActiveCompressionType != CompressionType.NoCompression && estimatedSize > CompressionThreshold)
                serConfig.CompressionType = ActiveCompressionType;

            // Should we compress data?
            if (serConfig.IsCompressed)
            {
                byte[] rawDataAggregated = new byte[storageFormatIdsSizeBytes.Length + storageFormatDataSizeBytes.Length + packedDataSizeBytes.Length +
                    StorageFormatIds_ByteArraySize + StorageFormatData_ByteArraySize + PackedData_ByteArraySize];

                int destIndex=0;

                // Store StorageFormatIds bytes
                byte[] tmpArray = storageFormatIdsSizeBytes;
                Array.Copy(tmpArray, 0, rawDataAggregated, destIndex, tmpArray.Length);
                destIndex += tmpArray.Length;

                StorageFormatIds.StoreBytesInByteArray(rawDataAggregated, destIndex);
                destIndex += StorageFormatIds_ByteArraySize;

                // Store StorageFormatData bytes
                tmpArray = storageFormatDataSizeBytes;
                Array.Copy(tmpArray, 0, rawDataAggregated, destIndex, tmpArray.Length);
                destIndex += tmpArray.Length;

                StorageFormatData.StoreBytesInByteArray(rawDataAggregated, destIndex);
                destIndex += StorageFormatData_ByteArraySize;

                // Store PackedData bytes
                tmpArray = packedDataSizeBytes;
                Array.Copy(tmpArray, 0, rawDataAggregated, destIndex, tmpArray.Length);
                destIndex += tmpArray.Length;

                PackedData.StoreBytesInByteArray(rawDataAggregated, destIndex);
                destIndex += PackedData_ByteArraySize;

                // Compressed result
                byte[] compressedContent = CompressionFactory.GetCompressionEngine(ActiveCompressionType).Compress(rawDataAggregated, ActiveCompressionLevel);

                // Encode config
                byte[] serConfigBytes = serConfig.ToByteArray();

                // Buffer for final result
                byte[] result = new byte[serConfigBytes.Length + compressedContent.Length];

                // Copy config info
                Array.Copy(serConfigBytes, 0, result, 0, serConfigBytes.Length);

                // Copy compressed content
                Array.Copy(compressedContent, 0, result, serConfigBytes.Length, compressedContent.Length);

                return result;
            }
            else
            {
                // No compression necessary - we store raw data

                // Encode config
                byte[] serConfigBytes = serConfig.ToByteArray();

                byte[] rawDataAggregated = new byte[serConfigBytes.Length + storageFormatIdsSizeBytes.Length + storageFormatDataSizeBytes.Length + packedDataSizeBytes.Length +
                    StorageFormatIds_ByteArraySize + StorageFormatData_ByteArraySize + PackedData_ByteArraySize];

                int destIndex=0;

                // Store Serialization Configuration bytes
                byte[] tmpArray = serConfigBytes;
                Array.Copy(tmpArray, 0, rawDataAggregated, destIndex, tmpArray.Length);
                destIndex += tmpArray.Length;

                // Store StorageFormatIds bytes
                tmpArray = storageFormatIdsSizeBytes;
                Array.Copy(tmpArray, 0, rawDataAggregated, destIndex, tmpArray.Length);
                destIndex += tmpArray.Length;

                StorageFormatIds.StoreBytesInByteArray(rawDataAggregated, destIndex);
                destIndex += StorageFormatIds_ByteArraySize;

                // Store StorageFormatData bytes
                tmpArray = storageFormatDataSizeBytes;
                Array.Copy(tmpArray, 0, rawDataAggregated, destIndex, tmpArray.Length);
                destIndex += tmpArray.Length;

                StorageFormatData.StoreBytesInByteArray(rawDataAggregated, destIndex);
                destIndex += StorageFormatData_ByteArraySize;

                // Store PackedData bytes
                tmpArray = packedDataSizeBytes;
                Array.Copy(tmpArray, 0, rawDataAggregated, destIndex, tmpArray.Length);
                destIndex += tmpArray.Length;

                PackedData.StoreBytesInByteArray(rawDataAggregated, destIndex);
                destIndex += PackedData_ByteArraySize;

                return rawDataAggregated;
            }
        }

        public void WriteStorageFormat(IStorageFormat storageFormat)
        {
            StorageFormatIds.StoreNextBits(storageFormat.FormatId, storageFormat.FormatIdSize);
            
            if (storageFormat.FormatConfig.SizeInBits>0)
                StorageFormatData.StoreNextBits(storageFormat.FormatConfig.Bits, storageFormat.FormatConfig.SizeInBits);
        }

        public void WritePackedData(byte[] packedData)
        {
            if (packedData == null || packedData.Length == 0)
                return;

            PackedData.StoreBytes(packedData);
        }

        public byte ReadStorageFormatId(byte idSize)
        {
            return StorageFormatIds.ReadNextId(idSize);
        }

        public uint ReadStorageFormatData(byte usedBits)
        {
            if (usedBits==0)
                return 0;

            return StorageFormatData.ReadNextBits(usedBits);
        }

        // Read bytes from PackedData store
        public byte[] ReadPackedData(int packedDataSize)
        {
            if (packedDataSize == 0)
                return null;

            return PackedData.ReadBytes(packedDataSize);
        }

        public ISerializerStorage SerializerStorage {get { return this; }}

        // Read one byte from PackedData store
        public byte ReadPackedDataByte()
        {
            return PackedData.ReadPackedDataByte();
        }
    }
}

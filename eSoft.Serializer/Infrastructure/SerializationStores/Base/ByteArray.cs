// -----------------------------------------------------------------------
// <copyright file="ByteArray.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.Infrastructure.SerializationStores.Base
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ByteArray
    {
        private const int MaxBytesPerCachedValue = 8; // for UInt64
        private const int MaxBitsPerCachedValue = 8 * MaxBytesPerCachedValue; // for UInt64

        // Byte Array data
        private List<UInt64> m_StorageArray;

        private int m_CachedValPosInStore;
        private int m_ByteShiftValue;

        public int CachedValPosInStore
        {
            get { return m_CachedValPosInStore; }
            set
            {
                if (m_CachedValPosInStore == m_StorageArray.Count)
                    m_StorageArray.Add(m_CachedValue);
                else
                    m_StorageArray[m_CachedValPosInStore] = m_CachedValue;

                m_CachedValPosInStore = value;
                m_CachedValue = m_StorageArray[value];
                m_ByteShiftValue = 0;
            }
        }

        // Cached values
        UInt64 m_CachedValue;

        // Default constructor
        public ByteArray()
        {
            m_StorageArray = new List<UInt64>();
            m_CachedValPosInStore = 0;
            m_ByteShiftValue = 0;
        }

        // Store bytes
        public void StoreBytes(byte[] packedData)
        {
            for (int pos = 0; pos < packedData.Length; pos++)
            {
                // Store byte on current position
                m_CachedValue = m_CachedValue | (((UInt64)packedData[pos]) << m_ByteShiftValue);

                m_ByteShiftValue += 8;
                m_ByteShiftValue %= 64;

                if (m_ByteShiftValue == 0)
                {
                    m_CachedValPosInStore++;
                    m_StorageArray.Add(m_CachedValue);
                    m_CachedValue = 0;
                }
            }
        }

        // Read next group of bytes
        public byte[] ReadBytes(int packedDataSize)
        {
            byte[] result = new byte[packedDataSize];

            for (int pos = 0; pos < packedDataSize; pos++)
            {
                result[pos] = (byte)(m_CachedValue >> m_ByteShiftValue);

                m_ByteShiftValue += 8;
                m_ByteShiftValue %= 64;

                if (m_ByteShiftValue == 0)
                {
                    m_CachedValPosInStore++;
                    m_CachedValue = m_StorageArray[m_CachedValPosInStore];
                }
            }

            return result;
        }

        // Read one byte from PackedData store
        public byte ReadPackedDataByte()
        {
            byte result = (byte)(m_CachedValue >> m_ByteShiftValue);

            m_ByteShiftValue += 8;
            m_ByteShiftValue %= 64;

            if (m_ByteShiftValue == 0)
            {
                m_CachedValPosInStore++;
                m_CachedValue = m_StorageArray[m_CachedValPosInStore];
            }

            return result;
        }

        private int m_FullStorageUnitsToStore;
        private int m_BytesOnIncompleteStorageUnitToStore;

        public int ByteArraySize
        {
            get
            {
                // Have it shadow value in backstore already ?
                if (m_CachedValPosInStore < m_StorageArray.Count)
                    m_StorageArray[m_CachedValPosInStore] = m_CachedValue;
                else
                    m_StorageArray.Add(m_CachedValue);

                m_FullStorageUnitsToStore = m_StorageArray.Count;
                m_BytesOnIncompleteStorageUnitToStore = 0;
                //int bytesToStore = m_ConfigBits.Count * MaxBytesPerCachedValue;

                // If the last one byte is not filled full then we have to store less bytes
                if (m_ByteShiftValue != 0)
                {
                    m_FullStorageUnitsToStore--; // We should store less full storage units
                    m_BytesOnIncompleteStorageUnitToStore = m_ByteShiftValue / 8; // Full bytes
                }

                // Calculate total size
                return m_FullStorageUnitsToStore * MaxBytesPerCachedValue + m_BytesOnIncompleteStorageUnitToStore;
            }
        }

        public void StoreBytesInByteArray(byte[] byteArray, int startPos)
        {
            // ByteArraySize.Get should WaitHandleCannotBeOpenedException called first tu update cache !!!

            // Copy bytes from full storage units
            int storageUnitPos = 0;
            for (; storageUnitPos < m_FullStorageUnitsToStore; storageUnitPos++)
            {
                UInt64 storageUnitValue = m_StorageArray[storageUnitPos];
                for (int bytePos = 0; bytePos < MaxBytesPerCachedValue; bytePos++)
                {
                    byteArray[startPos] = (byte)(storageUnitValue >> (bytePos * 8));
                    startPos++;
                }
            }

            // Copy bytes from partially filled storaged unit
            if (m_BytesOnIncompleteStorageUnitToStore > 0)
            {
                UInt64 storageUnitValue = m_StorageArray[storageUnitPos];
                for (int bytePos = 0; bytePos < m_BytesOnIncompleteStorageUnitToStore; bytePos++)
                {
                    byteArray[startPos] = (byte)(storageUnitValue >> (bytePos * 8));
                    startPos++;
                }
            }
        }

        public void InitFromSerializedData(IEnumerable<byte> serializedStore)
        {
            UInt64 storageUnit = 0;
            int byteShift = 0;

            foreach (byte byteElem in serializedStore)
            {
                storageUnit |= ((UInt64)byteElem) << byteShift;

                byteShift += 8;
                byteShift %= MaxBitsPerCachedValue;

                if (byteShift == 0)
                {
                    m_StorageArray.Add(storageUnit);
                    storageUnit = 0;
                }
            }

            // Store uncomplete storage unit
            if (byteShift != 0)
                m_StorageArray.Add(storageUnit);

            m_CachedValPosInStore = 0;
            m_ByteShiftValue = 0;

            if(m_StorageArray.Count>0)
                m_CachedValue = m_StorageArray[0];
        }
    }
}

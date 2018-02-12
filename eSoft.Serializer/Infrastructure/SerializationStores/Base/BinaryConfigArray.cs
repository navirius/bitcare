// -----------------------------------------------------------------------
// <copyright file="ConfigBinaryArray.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using eSoft.Serializer.Exceptions;
using System.Collections;

namespace eSoft.Serializer.Infrastructure.SerializationStores.Base
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BinaryConfigArray
    {
        private const int MaxBytesPerCachedValue = 8; // for UInt64
        private const int MaxBitsPerCachedValue = 8 * MaxBytesPerCachedValue; // for UInt64

        // Binary map
        private List<UInt64> m_StorageArray;

        // Index of next free bit
        private int m_CachedValPosInStore;
        private int m_BitPositionInCachedVal;

        public int CachedValPosInStore 
        { 
            get { return m_CachedValPosInStore; } 
            set {
                if (m_CachedValPosInStore == m_StorageArray.Count)
                    m_StorageArray.Add(m_CachedValue);
                else
                    m_StorageArray[m_CachedValPosInStore] = m_CachedValue;

                m_CachedValPosInStore = value; 
                m_CachedValue = m_StorageArray[value]; 
                m_BitPositionInCachedVal = 0; } 
        }

        public int BitPositionInCachedVal { get { return m_BitPositionInCachedVal; } set { m_BitPositionInCachedVal = value; } }

        // Cached values
        UInt64 m_CachedValue;
        
        uint m_CachedMask4Length;
        int m_CachedMask4LengthSize;

        // Modification guard for parrallel operations
        //private ReaderWriterLockSlim m_Lock;

        // Constructor
        public BinaryConfigArray()
        {
            m_StorageArray = new List<UInt64>();

            // Cached value
            m_CachedValue = 0;

            // Initial position of next free bit in bit map
            m_CachedValPosInStore = 0;
            m_BitPositionInCachedVal = 0;

            m_CachedMask4Length = 0xffffffff % (1u << 8);
            m_CachedMask4LengthSize = 8; // Initial mask is for 1 byte = 8 bits

            //m_Lock = new ReaderWriterLockSlim();
        }

        // Stores group of bits (length bits) - for sequencial processing - assumption: cache value is always up to date
        public void StoreNextBits(uint bits, byte length)
        {
            // Update length mask
            if (m_CachedMask4LengthSize != length)
            {
                m_CachedMask4Length = 0xffffffff % (1u << length);
                m_CachedMask4LengthSize = length;
            }

            // Calculate mask to apply bits to
            UInt64 mask = ~(((UInt64)m_CachedMask4Length) << m_BitPositionInCachedVal);

            // Shift value bits to proper position according to prepared mask
            UInt64 shiftedValueBits = ((UInt64)bits) << m_BitPositionInCachedVal;

            // Calculate how many bits are missing
            int freeBitsOnCurrentStorageUnit = MaxBitsPerCachedValue - m_BitPositionInCachedVal;
            int remainingBits = length - freeBitsOnCurrentStorageUnit;

            // Do we have to modify more storage units then the current one only?
            if (remainingBits > 0)
            {
                // Two storage units to modify

                // Modify current free bits on cache (lower bits of shiftedValueBits - mask will adopt automatically))
                m_StorageArray.Add(m_CachedValue & mask | shiftedValueBits);

                // Store remaining bits (except for bits stored already on previous storage unit - we remove them by shift operation)
                m_CachedValue = bits >> freeBitsOnCurrentStorageUnit;

                // Update position pointer
                m_CachedValPosInStore++;
                m_BitPositionInCachedVal = (m_BitPositionInCachedVal + length) % MaxBitsPerCachedValue;
            }
            else
            {
                // One storage units to modify
                m_CachedValue = m_CachedValue & mask | shiftedValueBits;

                // Update position pointer
                m_BitPositionInCachedVal += length;
                m_BitPositionInCachedVal %= MaxBitsPerCachedValue;

                // Have we landed on next storage unit (bit 0) after modification of current one storage unit?
                if (m_BitPositionInCachedVal == 0)
                {
                    m_StorageArray.Add(m_CachedValue);

                    m_CachedValue = 0;
                    m_CachedValPosInStore++;
                }
            }
        }

        // Read group of bits (length bits) at current position in bit array - for sequencial processing
        public uint ReadNextBits(byte length)
        {
            // Update length mask
            if (m_CachedMask4LengthSize != length)
            {
                m_CachedMask4Length = 0xffffffff % (1u << length);
                m_CachedMask4LengthSize = length;
            }

            // Calculate how many bits are missing
            int unreadBitsOnCurrentStorageUnit = MaxBitsPerCachedValue - m_BitPositionInCachedVal;
            int remainingBits = length - unreadBitsOnCurrentStorageUnit;

            // Do we have to read more storage units then the current one only?
            if (remainingBits > 0)
            {
                // We have to read value from two storage units

                // Read bits on currently cached value
                uint bits = (uint)(m_CachedValue >> m_BitPositionInCachedVal);

                // Update cache and pointer
                m_CachedValPosInStore++;
                m_BitPositionInCachedVal = remainingBits;
                m_CachedValue = m_StorageArray[m_CachedValPosInStore];

                // Set remaining bits on proper position
                bits= bits | (uint)((m_CachedValue & (m_CachedMask4Length >> unreadBitsOnCurrentStorageUnit)) << unreadBitsOnCurrentStorageUnit);
                return bits;
            }
            else
            {
                // We have to read bits from cached value only
                uint bits = (uint)(m_CachedValue >> m_BitPositionInCachedVal);

                // Update position pointer
                m_BitPositionInCachedVal += length;
                m_BitPositionInCachedVal %= MaxBitsPerCachedValue;

                // Have we landed on next storage unit (bit 0) after modification of current one storage unit?
                if (m_BitPositionInCachedVal == 0)
                {
                    m_CachedValPosInStore++;
                    m_CachedValue = m_StorageArray[m_CachedValPosInStore];
                }

                // Mask final result
                bits = bits & m_CachedMask4Length;
                return bits;
            }
        }

        private int m_FullStorageUnitsToStore;
        private int m_BytesOnIncompleteStorageUnitToStore;

        public int GetByteArraySize()
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
            if (m_BitPositionInCachedVal != 0)
            {
                m_FullStorageUnitsToStore--; // We should store less full storage units
                m_BytesOnIncompleteStorageUnitToStore = m_BitPositionInCachedVal / 8; // Full bytes

                // Remaining bits on uncomplete byte (only a few bits are set on this byte)
                if (m_BitPositionInCachedVal % 8 > 0)
                    m_BytesOnIncompleteStorageUnitToStore++;
            }

            // Calculate total size
            return m_FullStorageUnitsToStore * MaxBytesPerCachedValue + m_BytesOnIncompleteStorageUnitToStore; 
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
            if (byteShift!=0)
                m_StorageArray.Add(storageUnit);

            m_CachedValPosInStore = 0;
            m_BitPositionInCachedVal = 0; 

            if(m_StorageArray.Count>0)
                m_CachedValue = m_StorageArray[0];
        }
    }
}

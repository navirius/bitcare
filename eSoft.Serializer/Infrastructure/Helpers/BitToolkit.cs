// -----------------------------------------------------------------------
// <copyright file="IntToolkit.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.Infrastructure.Helpers
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class BitToolkit
    {
        public static byte[] UInt64ToByteArray(UInt64 value)
        {
            return new byte[8] {    (byte)value, (byte)(value >> 8), (byte)(value >> 16), (byte)(value >> 24), 
                                    (byte)(value>>32), (byte)(value >> 40), (byte)(value >> 48), (byte)(value >> 56)};

            //return BitConverter.GetBytes(value);
        }

        public static UInt64 ByteArrayToUInt64(byte[] byteArray)
        {
            //ushort value1 = (ushort)(byteArray[0] | (byteArray[1] << 8) | (byteArray[2] << 16) | (byteArray[3] << 24));
            //ushort value2 = (ushort)(byteArray[4] | (byteArray[5] << 8) | (byteArray[6] << 16) | (byteArray[7] << 24));
            //return (UInt64)(value1 | (value2 << 32));

            return (UInt64)(byteArray[0] | (byteArray[1] << 8) | (byteArray[2] << 16) | (byteArray[3] << 24) |
                (byteArray[4] << 32) | (byteArray[5] << 40) | (byteArray[6] << 48) | (byteArray[7] << 56));

            //return BitConverter.ToUInt64(byteArray, 0);
        }

        // Size of array says on how many bytes we stored value
        public static byte[] EncodeSize(Int32 value)
        {
            byte[] sizeBytes = ConvertInt32ToByteArray(value);
            byte[] outputBytes = new byte[sizeBytes.Length + 1];

            outputBytes[0] = (byte)sizeBytes.Length;
            Array.Copy(sizeBytes, 0, outputBytes, 1, sizeBytes.Length);

            return outputBytes;
        }

        // Size of array says on how many bytes we stored value
        public static Int32 DecodeSize(byte[] packedData, out int bytesRead, int startIndex = 0)
        {
            int valueSizeInBytes = packedData[startIndex];
            startIndex++;

            // Inital value
            Int32 result = 0;

            // Conversion
            for (int pos = 0; pos < valueSizeInBytes; pos++)
                result += packedData[pos + startIndex] << (8 * pos);

            // Info about bytes read
            bytesRead = valueSizeInBytes + 1;

            // Return result
            return result;
        }

        // Size of array says on how many bytes we stored value
        public static byte[] ConvertInt32ToByteArray(Int32 value)
        {
            // Buffer for bytes representation
            byte[] tmpBuf = new byte[4];

            // Store bytes in buffer
            int pos = 0;
            for (; pos < 4; pos++)
            {
                tmpBuf[pos] = (byte)value;

                value >>= 8;
                if (value == 0)
                    break;
            }

            // Prepare output array
            byte[] storedBytes = new byte[pos + 1];
            Array.Copy(tmpBuf, 0, storedBytes, 0, pos + 1);

            // Return result
            return storedBytes;
        }

        // Size of array says on how many bytes we stored value
        public static Int32 ConvertByteArrayToInt32(byte[] packedData, int startIndex = 0)
        {
            // Inital value
            Int32 result = 0;

            // Conversion
            for (int pos = startIndex; pos < packedData.Length; pos++)
                result += packedData[pos] << 8 * (pos - startIndex);

            // Return result
            return result;
        }

        // Size of array says on how many bytes we stored value
        public static byte[] ConvertInt64ToByteArray(Int64 value)
        {
            // Buffer for bytes representation
            byte[] tmpBuf = new byte[8];

            // Store bytes in buffer
            int pos = 0;
            for (; pos < 8; pos++)
            {
                tmpBuf[pos] = (byte)value;

                value >>= 8;
                if (value == 0)
                    break;
            }

            // Prepare output array
            byte[] storedBytes = new byte[pos + 1];
            Array.Copy(tmpBuf, 0, storedBytes, 0, pos + 1);

            // Return result
            return storedBytes;
        }

        // Size of array says on how many bytes we stored value
        public static Int64 ConvertByteArrayToInt64(byte[] packedData, int startIndex = 0)
        {
            // Inital value
            Int64 result = 0;

            // Conversion
            for (int pos = startIndex; pos < packedData.Length; pos++)
                result += ((Int64)packedData[pos]) << (8 * (pos - startIndex));

            // Return result
            return result;
        }

        // Size of array says on how many bytes we stored value
        public static byte[] ConvertDoubleToByteArray(Double value)
        {
            // Buffer for bytes representation
            byte[] tmpBytes = new byte[9]; // 8 bytes of Int64 + config byte at (0)
            int storedTmpBytes = 1;

            // Double bits
            Int64 int64Bits = BitConverter.DoubleToInt64Bits(value);
            byte[] doubleBytes = ConvertInt64ToByteArray(int64Bits);

            // We mark on every bit if some byte has value or not
            byte configByte = 0;

            // Do we have all the bytes in input array?
            int indexOfLastByte = (doubleBytes.Length < 8) ? doubleBytes.Length : 8;

            // Store bytes in buffer
            for (int pos = 0; pos < indexOfLastByte; pos++)
            {
                if (doubleBytes[pos] > 0)
                {
                    configByte |= (byte)(1 << pos); // If byte is different then 0
                    tmpBytes[storedTmpBytes] = doubleBytes[pos];
                    storedTmpBytes++;
                }
            }

            // Update config byte in array
            tmpBytes[0] = configByte;

            byte[] storedBytes = new byte[storedTmpBytes];
            Array.Copy(tmpBytes, 0, storedBytes, 0, storedTmpBytes);

            // Return result
            return storedBytes;
        }

        public static Double ConvertByteArrayToDouble(byte[] packedData, int startIndex = 0)
        {
            // Double bits
            Int64 int64Bits = 0;

            // Read config byte
            byte config = packedData[startIndex];

            // Skip config byte
            startIndex++;

            // Conversion - we check out bits for all the bytes of Int64 type (8 bytes)
            for (int bitPos = 0; bitPos < 8; ++bitPos)
            {
                // If bit is set (has value 1) then we put this byte on proper position
                if ((config & (1 << bitPos)) > 0)
                {
                    // Shift byto into proper position
                    int64Bits += ((Int64)packedData[startIndex]) << (8 * bitPos);
                    startIndex++;
                }
            }

            // Return result
            return BitConverter.Int64BitsToDouble(int64Bits);
        }

        public static byte[] ConvertGuidToByteArray(Guid valueToSerialize)
        {
            return valueToSerialize.ToByteArray();
        }

        public static Guid ConvertByteArrayToGuid(byte[] serializedData)
        {
            return new Guid(serializedData);
        }

        public static byte[] ConvertInt16ToByteArray(Int16 value)
        {
            // Buffer for bytes representation
            byte[] tmpBuf = new byte[2];

            // Store bytes in buffer
            int pos = 0;
            for (; pos < 2; pos++)
            {
                tmpBuf[pos] = (byte)value;

                value >>= 8;
                if (value == 0)
                    break;
            }

            // Prepare output array
            byte[] storedBytes = new byte[pos + 1];
            Array.Copy(tmpBuf, 0, storedBytes, 0, pos + 1);

            // Return result
            return storedBytes;
        }

        public static Int16 ConvertByteArrayToInt16(byte[] packedData, int startIndex = 0)
        {
            // Inital value
            short result = 0;

            // Conversion
            for (int pos = startIndex; pos < packedData.Length; pos++)
                result += (short)(packedData[pos] << 8 * (pos - startIndex));

            // Return result
            return result;
        }

        public static byte[] ConvertSingleToByteArray(Single valueToSerialize)
        {
            byte[] singleBytes=BitConverter.GetBytes(valueToSerialize);
            
            byte configByte = 0;
            byte[] tmpBytes = new byte[5]; // 4 bytes of Single + config byte
            int storedTmpBytes = 1;

            // Store bytes in buffer
            for (int pos = 0; pos < 4; pos++)
            {
                if (singleBytes[pos] > 0)
                {
                    configByte |= (byte)(1 << pos); // If byte is different then 0
                    tmpBytes[storedTmpBytes] = singleBytes[pos]; // Copy byte to output list
                    storedTmpBytes++;
                }
            }

            // Update config byte in array
            tmpBytes[0] = configByte;

            byte[] outputBytes = new byte[storedTmpBytes];
            Array.Copy(tmpBytes, 0, outputBytes, 0, storedTmpBytes);

            // Return result
            return outputBytes;
        }

        public static Single ConvertByteArrayToSingle(byte[] packedData, int startIndex=0)
        {
            // Single bytes
            byte[] singleBytes = new byte[4];

            // Read config byte
            byte config = packedData[startIndex];

            // Skip config byte
            startIndex++;

            // Conversion - we check out bits for all the bytes (4 bytes)
            for (int bitPos = 0; bitPos < 4; ++bitPos)
            {
                // If bit is set (has value 1) then we put this byte on proper position
                if ((config & (1 << bitPos)) > 0)
                {
                    // Shift byto into proper position
                    singleBytes[bitPos] = packedData[startIndex];
                    startIndex++;
                }
            }

            // Return result
            return BitConverter.ToSingle(singleBytes, 0);
        }
    }
}

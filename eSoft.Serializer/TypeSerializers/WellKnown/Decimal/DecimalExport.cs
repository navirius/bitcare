using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Compression;
using System.IO;
using eSoft.Serializer.Infrastructure.Helpers;

namespace eSoft.Serializer
{
    public partial class Serializer
    {
        // Serialization of Decimal
        public static byte[] SerializeDecimal(Decimal valueToSerialize)
        {
            // Default value has byte 0 only
            if (valueToSerialize == Decimal.Zero)
                return new byte[1] { 0 };

            // Value different then default one
            int[] fourInt32Values = Decimal.GetBits(valueToSerialize);

            byte[] tmpDecimalBytes = new byte[16]; // 4 x Int32 bytes

            Array.Copy(BitConverter.GetBytes(fourInt32Values[0]), 0, tmpDecimalBytes, 0, 4);
            Array.Copy(BitConverter.GetBytes(fourInt32Values[1]), 0, tmpDecimalBytes, 4, 4);
            Array.Copy(BitConverter.GetBytes(fourInt32Values[2]), 0, tmpDecimalBytes, 8, 4);
            Array.Copy(BitConverter.GetBytes(fourInt32Values[3]), 0, tmpDecimalBytes, 12, 4);

            // We set bit when byte on n position is different then 0
            ushort configBits = 0;

            byte[] tmpOutputBytes = new byte[16]; // 16 decimal bytes max
            int storedTmpOutputBytes = 0;

            // Set bit info for all the bytes
            for (int pos = 0; pos < 16; pos++)
            {
                if (tmpDecimalBytes[pos] > 0)
                {
                    configBits |= (ushort)(1 << pos);
                    tmpOutputBytes[storedTmpOutputBytes] = tmpDecimalBytes[pos];
                    storedTmpOutputBytes++;
                }
            }

            // Output buffer
            byte[] packedData = new byte[2 + storedTmpOutputBytes]; // 2 config bytes + stored decimal bytes (according to config value)

            // Config info
            packedData[0] = (byte)(configBits & 0xff);
            packedData[1] = (byte)((configBits >> 8) & 0xff);

            Array.Copy(tmpOutputBytes, 0, packedData, 2, storedTmpOutputBytes);

            // Return final result
            return packedData;
        }

        // Deserialization of Decimal
        public static Decimal DeserializeDecimal(byte[] serializedData)
        {
            // Default value has byte 0 only
            if (serializedData.Length == 1 && serializedData[0] == 0)
                return Decimal.Zero;

            // Config bits
            ushort configBits = (ushort)(serializedData[0] + 256 * serializedData[1]);

            int bufPos=2;
            int[] decimalBuffer = new int[4];

            for(int intPos=0;intPos<4;intPos++)
            {
                for(int bytePos=0;bytePos<4;bytePos++)
                {
                    // If selected one bit (from 16) has been set then we should read value of coresponding byte and apply it on proper position in buffer of ints
                    if ((configBits & (1 << (intPos * 4 + bytePos))) > 0)
                    {
                        decimalBuffer[intPos] |= serializedData[bufPos] << (8 * bytePos);
                        bufPos++;
                    }
                }
            }
            
            return new Decimal(decimalBuffer);
        }
    }
}

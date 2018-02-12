using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;
using eSoft.Serializer.TypeSerializers.WellKnown.Decimal_StorageFormats;

namespace eSoft.Serializer.TypeSerializers.WellKnown
{
    public class DecimalSerializer : SerializedObject,IObjectSerializer<Decimal>
    {
        // Constructor
        public DecimalSerializer(ISerializerStorage serializerStorage) : base(serializerStorage) { }

        // Serialization
        public void Serialize(Decimal valueToSerialize)
        {
            // Is it default value
            if (valueToSerialize == Decimal.Zero)
            {
                SerializerStorage.WriteStorageFormat(new DefaultValue());
                return;
            }

            // Is it negative value
            bool isNegativeValue = valueToSerialize < 0;
            if (isNegativeValue)
                valueToSerialize *= (-1);

            // Value different then default one
            int[] fourInt32Values = Decimal.GetBits(valueToSerialize);

            byte[] tmpDecimalBytes = new byte[16]; // 4 x Int32 bytes

            Array.Copy(BitConverter.GetBytes(fourInt32Values[0]), 0, tmpDecimalBytes, 0, 4);
            Array.Copy(BitConverter.GetBytes(fourInt32Values[1]), 0, tmpDecimalBytes, 4, 4);
            Array.Copy(BitConverter.GetBytes(fourInt32Values[2]), 0, tmpDecimalBytes, 8, 4);
            Array.Copy(BitConverter.GetBytes(fourInt32Values[3]), 0, tmpDecimalBytes, 12, 4);

            int outputBytesCounter = 0;
            
            // Count valid bytes
            for (int pos = 0; pos < 16; pos++)
            {
                if (tmpDecimalBytes[pos] > 0)
                    outputBytesCounter++;

                if (tmpDecimalBytes[pos] == 0)
                    break;
            }

            // Output buffer
            byte[] packedData = new byte[outputBytesCounter]; // stored decimal bytes
            Array.Copy(tmpDecimalBytes, 0, packedData, 0, outputBytesCounter);

            // Is it PositiveValueInDataStream storage case
            if (isNegativeValue)
                SerializerStorage.WriteStorageFormat(new NegativeValueInDataStream((byte)packedData.Length));
            else
                // Has positive value
                SerializerStorage.WriteStorageFormat(new PositiveValueInDataStream((byte)packedData.Length));

            SerializerStorage.WritePackedData(packedData);
        }

        // Deserialization
        public Decimal Deserialize()
        {
            // Read info about storage format
            DecimalStorageFormats format = (DecimalStorageFormats)SerializerStorage.ReadStorageFormatId(DecimalStorageBase.FormatIdSizeInBits);

            // Is it default value
            if (format == DecimalStorageFormats.DefaultValue)
                return Decimal.Zero;

            // Size of data in buffer
            PositiveValueInDataStream positiveValueInDataStream = new PositiveValueInDataStream();
            positiveValueInDataStream.FormatConfig.Bits = SerializerStorage.ReadStorageFormatData(PositiveValueInDataStream.UsedConfigBitsForCase);
            byte packedDataSize = (byte)positiveValueInDataStream.PackedDataSize;

            // Data
            byte[] packedData = SerializerStorage.ReadPackedData(packedDataSize);

            // Buffer
            int[] decimalBuffer = new int[4];

            int intPos = 0;
            int byteShift = 0;

            // Restore value
            for (int pos = 0; pos < packedDataSize; pos++)
            {
                decimalBuffer[intPos] |= packedData[pos] << byteShift;

                byteShift += 8;
                byteShift %= 8;

                if (byteShift == 0)
                    intPos++;
            }

            // Is it NegativeValueInDataStream) storage case
            if (format == DecimalStorageFormats.NegativeValueInDataStream)
            {
                return new Decimal(decimalBuffer) * (-1);
            }

            // Is it PositiveValueInDataStream storage case
            // DecimalStorageFormats.PositiveValueInDataStream
            return new Decimal(decimalBuffer);
        }
    }
}

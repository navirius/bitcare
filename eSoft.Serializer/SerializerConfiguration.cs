// -----------------------------------------------------------------------
// <copyright file="SerializerConfiguration.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Compression;
using System.IO;

namespace eSoft.Serializer
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SerializerConfiguration
    {
        // Binary encoded value of serializer configuration
        private byte m_EncodedValue = 0;
        public byte EncodedValue 
        {
            get { return m_EncodedValue; }
            set
            {
                m_EncodedValue = value;
                m_IsCompressed = false;

                // Value without CompressionBit
                m_SerializerFormatVersion = (byte)(m_EncodedValue & (~c_CompressionBit));

                if ((m_EncodedValue & c_CompressionBit) > 0)
                    m_IsCompressed = true;
            }
        }

        // b7 is compression bit
        private const byte c_CompressionBit = 0x80;
        private const byte c_CompressionBitMask = (~c_CompressionBit) & 0xff; // Limited to one byte only

        private bool m_IsCompressed = false;
        public bool IsCompressed
        {
            get { return m_IsCompressed; }
            private set
            {
                m_IsCompressed = value;

                if (m_IsCompressed)
                    m_EncodedValue |= c_CompressionBit;
                else
                    m_EncodedValue &= c_CompressionBitMask;
            }
        }

        private byte m_SerializerFormatVersion = 0;
        public byte SerializerFormatVersion 
        {
            get { return m_SerializerFormatVersion; }
            set
            {
                m_SerializerFormatVersion = value;
                m_EncodedValue &= c_CompressionBit; // Leave compression bit only - mask format bits
                m_EncodedValue |= m_SerializerFormatVersion;
            }
        }

        private CompressionType m_CompressionType = CompressionType.NoCompression;
        public CompressionType CompressionType 
        {
            get { return m_CompressionType; } 
            set
            {
                m_CompressionType = value;

                if (m_CompressionType != CompressionType.NoCompression)
                    IsCompressed = true;
                else
                    IsCompressed = false;
            }
        }

        // On how many bytes do we store serializer configuration?
        public int StorageSize // In bytes
        {
            get
            {
                // Configuration with additional info byte about used compression type (compressor type)
                if (IsCompressed)
                    return 2;

                // Configuration without compression info
                return 1;
            }
        }

        // Serialization
        public byte[] ToByteArray()
        {
            // Configuration with info about used compression type
            if (IsCompressed)
                return new byte[2] { EncodedValue, (byte)m_CompressionType };

            // Single byte configuration
            return new byte[1] { EncodedValue };
        }

        // Deserialization
        public static SerializerConfiguration FromByteArray(byte[] byteArray, out int readBytes)
        {
            SerializerConfiguration config = new SerializerConfiguration();
            config.EncodedValue = byteArray[0];
            readBytes = 1;

            if (config.IsCompressed)
            {
                config.CompressionType = (CompressionType)byteArray[1];
                readBytes++;
            }

            return config;
        }
    }
}

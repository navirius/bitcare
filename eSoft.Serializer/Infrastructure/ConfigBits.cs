// -----------------------------------------------------------------------
// <copyright file="ConfigBits.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.Infrastructure
{
    public class ConfigBits
    {
        // Config bits value
        public uint Bits; // We can use 24 bits only or we have to use 64 bit ints to do calculations (slower on 32 bit systems)

        // Size of config bits in bits
        private byte m_SizeInBits = 0;
        public  byte SizeInBits 
        {
            get { return m_SizeInBits; }
            set { 
                m_SizeInBits = value;
                
            } 
        }

        // To speed up calculations
        private byte m_LastKnownSizeInBits;

        // We can set this value through SizeInBits property only
        private byte m_SizeInBytes;
        public  byte   SizeInBytes 
        {
            get 
            {
                // Calculate new value if differs from previous one...
                if (m_LastKnownSizeInBits != m_SizeInBits)
                {
                    m_LastKnownSizeInBits = m_SizeInBits;
                    m_SizeInBytes = (byte)(m_SizeInBits / 8);

                    if (m_SizeInBits % 8 > 0)
                        ++m_SizeInBytes;
                }

                return m_SizeInBytes; 
            } 
        }
    }
}

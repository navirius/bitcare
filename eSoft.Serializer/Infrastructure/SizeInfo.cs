// -----------------------------------------------------------------------
// <copyright file="Size.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure.Helpers;

namespace eSoft.Serializer.Infrastructure
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SizeInfo
    {
        // b7 and b6 bits contains info about the way we store size. 
        // 00 means size is encoded in ConfigBits (bits b5-b0)
        // Other value is size in bytes of stored size info on separate bytes
        public byte ConfigBits;

        // Bytes we use to store info about the size
        public byte[] SeparateBytes = null;

        private int m_Value = 0;
        public  int   Value {
            get { return m_Value; }
            set
            {
                m_Value = value;

                // Can we encode size in config byte
                if (m_Value < 128)
                {
                    ConfigBits = (byte)m_Value;
                    SeparateBytes = null;
                }
                else
                {
                    // We can't encode size in config byte so we store it as separate bytes
                    int valueToStore = m_Value;
                    int bytesWeNeed = SizeCalculator.GetInt32SizeInBytes(valueToStore);
                    SeparateBytes = new byte[bytesWeNeed];
                    
                    for (int pos = 0; pos < bytesWeNeed; pos++)
                    {
                        SeparateBytes[pos] = (byte)valueToStore;
                        valueToStore >>= 8;
                    }
                }
            }
        }
    }
}

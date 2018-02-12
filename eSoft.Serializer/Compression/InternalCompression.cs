// -----------------------------------------------------------------------
// <copyright file="QuickLZCompression.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.Compression
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class InternalCompression:ICompression
    {
        // Compression
        public byte[] Compress(byte[] dataBuffer, int level = 1)
        {
            return QuickLZSharp.QuickLZ.compress(dataBuffer, level);
        }

        // Decompression
        public byte[] Decompress(byte[] dataBuffer)
        {
            return QuickLZSharp.QuickLZ.decompress(dataBuffer);
        }
    }
}

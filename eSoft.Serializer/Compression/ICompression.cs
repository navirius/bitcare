// -----------------------------------------------------------------------
// <copyright file="ICompress.cs" company="">
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
    public interface ICompression
    {
        byte[] Compress(byte[] dataBuffer, int level = 1);
        byte[] Decompress(byte[] dataBuffer);
    }
}

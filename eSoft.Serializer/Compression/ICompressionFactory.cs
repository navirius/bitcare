// -----------------------------------------------------------------------
// <copyright file="ICompressionFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.Compression
{
    public enum CompressionType { NoCompression, Internal };

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICompressionFactory
    {
        ICompression GetCompressionEngine(CompressionType typeOfEngine);
    }
}

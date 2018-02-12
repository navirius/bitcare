using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Exceptions;

namespace eSoft.Serializer.Compression
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CompressionFactory : ICompressionFactory
    {
        // Known compression engines
        private ICompression m_NoCompression = null;
        private ICompression m_QuickLZ = null;

        public ICompression GetCompressionEngine(CompressionType typeOfEngine)
        {
            switch (typeOfEngine)
            {
                case CompressionType.NoCompression:
                    return m_NoCompression;

                case CompressionType.Internal:
                    if (m_QuickLZ == null)
                        m_QuickLZ=new InternalCompression();
                    return m_QuickLZ;

                default:
                    throw new UnsupportedCompressionTypeException("Type is " + typeOfEngine.ToString());
            }
        }
    }
}

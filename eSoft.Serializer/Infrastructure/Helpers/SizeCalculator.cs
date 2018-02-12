// -----------------------------------------------------------------------
// <copyright file="SizeCalculator.cs" company="">
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
    public static class SizeCalculator
    {
        public static int GetShortSizeInBytes(short value)
        {
            if (value == 0)
                return 0;

            // Remove bits 0-7 (so divide by 256)
            value >>= 8;
            if (value == 0) // There are no remaining bits - it can be stored on this amount of bytes
                return 1;

            // There is no other option then storage of all the bytes
            return 2;
        }

        public static int GetInt32SizeInBytes(Int32 value)
        {
            if (value == 0)
                return 0;

            // Remove bits 0-7 (so divide by 256)
            value >>= 8;
            if (value == 0) // There are no remaining bits - it can be stored on this amount of bytes
                return 1;

            // Remove bits 8-15
            value >>= 8;
            if (value == 0) // There are no remaining bits - it can be stored on this amount of bytes
                return 2;

            // Remove bits 16-23
            value >>= 8;
            if (value == 0) // There are no remaining bits - it can be stored on this amount of bytes
                return 3;

            // There is no other option then storage of all the bytes
            return 4;
        }

        public static int GetLongSizeInBytes(long value) // Int64
        {
            if (value == 0)
                return 0;

            // Remove bits 0-7 (so divide by 256)
            value >>= 8;
            if (value == 0) // There are no remaining bits - it can be stored on this amount of bytes
                return 1;

            // Remove bits 8-15
            value >>= 8;
            if (value == 0) // There are no remaining bits - it can be stored on this amount of bytes
                return 2;

            // Remove bits 16-23
            value >>= 8;
            if (value == 0) // There are no remaining bits - it can be stored on this amount of bytes
                return 3;

            // Remove bits 24-31
            value >>= 8;
            if (value == 0) // There are no remaining bits - it can be stored on this amount of bytes
                return 4;

            // Remove bits 32-39
            value >>= 8;
            if (value == 0) // There are no remaining bits - it can be stored on this amount of bytes
                return 5;

            // Remove bits 40-47
            value >>= 8;
            if (value == 0) // There are no remaining bits - it can be stored on this amount of bytes
                return 6;

            // Remove bits 48-55
            value >>= 8;
            if (value == 0) // There are no remaining bits - it can be stored on this amount of bytes
                return 7;

            // Bits 56-63
            // There is no other option then storage of all the bytes
            return 8;
        }
    }
}

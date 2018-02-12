// -----------------------------------------------------------------------
// <copyright file="eSoftSerializer.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using eSoft.Serializer.Infrastructure;

namespace eSoft.Serializer.SerializationUnits
{
    public class BaseSerializationUnit
    {
        public RuntimeTypeHandle TypeHandle;

        public bool HasNullValue;
        public bool HasDefaultValue;
        public bool IsRegisteredAlready;

        public int          ObjectId;
        public ConfigBits   Config = new ConfigBits();
        public SizeInfo     RealSize   = new SizeInfo();

        public BaseSerializationUnit[] Elems;
        public MemoryStream DataStream;
    }
}

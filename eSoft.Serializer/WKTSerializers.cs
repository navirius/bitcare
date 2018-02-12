// -----------------------------------------------------------------------
// <copyright file="StaticSerializers.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.ObjectDictionaries.ValType;
using eSoft.Serializer.TypeSerializers.WellKnown;

namespace eSoft.Serializer
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class WKTSerializers
    {
        public BooleanSerializer Boolean;
        public ByteSerializer Byte;
        public CharSerializer Char;
        public DateTimeSerializer DateTime;
        public DateTimeOffsetSerializer DateTimeOffset;
        public DecimalSerializer Decimal;
        public DoubleSerializer Double;
        public GuidSerializer Guid;
        public Int16Serializer Int16;
        public Int32Serializer Int32;
        public Int64Serializer Int64;
        public ObjectSerializer Object;
        public SByteSerializer SByte;
        public SingleSerializer Single;
        public StringSerializer String;
        public TimeSpanSerializer TimeSpan;
        public UInt16Serializer UInt16;
        public UInt32Serializer UInt32;
        public UInt64Serializer UInt64;

        public WKTSerializers(ISerializerStorage serializerStorage, IValueTypeObjectsDictionary objectCache)
        {
            Boolean = new BooleanSerializer(serializerStorage);
            Byte = new ByteSerializer(serializerStorage);
            Char = new CharSerializer(serializerStorage);
            DateTime = new DateTimeSerializer(serializerStorage);
            DateTimeOffset= new DateTimeOffsetSerializer(serializerStorage);
            Decimal= new DecimalSerializer(serializerStorage);
            Double= new DoubleSerializer(serializerStorage);
            Guid= new GuidSerializer(serializerStorage);
            Int16 = new Int16Serializer(serializerStorage);
            Int32= new Int32Serializer(serializerStorage);
            Int64 = new Int64Serializer(serializerStorage);
            Object = new ObjectSerializer(serializerStorage);
            SByte = new SByteSerializer(serializerStorage);
            Single = new SingleSerializer(serializerStorage);
            String = new StringSerializer(serializerStorage, objectCache);
            TimeSpan= new TimeSpanSerializer(serializerStorage);
            UInt16 = new UInt16Serializer(serializerStorage);
            UInt32= new UInt32Serializer(serializerStorage);
        }
    }
}

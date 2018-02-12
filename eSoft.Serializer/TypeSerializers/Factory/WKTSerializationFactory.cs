// -----------------------------------------------------------------------
// <copyright file="WKTSerializationFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.TypeSerializers.WellKnown;
using eSoft.Serializer.ObjectDictionaries.ValType;
using eSoft.Serializer.ObjectDictionaries.RefType;

namespace eSoft.Serializer.TypeSerializers.Factory
{
    #region Support classes

    public enum SerializerKind { Normal, CachedVal, CachedRef }

    public class BaseSerializerInfo<T>
    {
        public SerializerKind SerializerKind;
        public Func<T, byte[]> SimpleSerializerFunc;
        public Func<byte[], T> SimpleDeserializerFunc;
    }

    public class NormalObjectSerializerInfo<T> : BaseSerializerInfo<T>
    {
        // Default constructor
        public NormalObjectSerializerInfo() { this.SerializerKind = Factory.SerializerKind.Normal; }

        // Factory function
        public Func<ISerializerStorage, IObjectSerializer<T>> FactoryFunc;
    }

    public class CachedObjectSerializerInfo<T, TDictType> : BaseSerializerInfo<T>
    {
        // Factory function
        public Func<ISerializerStorage, TDictType, ICachedObjectSerializer<T>> FactoryFunc;
    }

    public class CachedValObjectSerializerInfo<T> :  CachedObjectSerializerInfo<T, IValueTypeObjectsDictionary>
    {
        // Default constructor
        public CachedValObjectSerializerInfo() { this.SerializerKind = Factory.SerializerKind.CachedVal; }
    }

    public class CachedRefObjectSerializerInfo<T> : CachedObjectSerializerInfo<T, IRefTypeObjectsDictionary>
    {
        // Default constructor
        public CachedRefObjectSerializerInfo() { this.SerializerKind = Factory.SerializerKind.CachedRef; }
    }

    #endregion


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class WKTSerializationFactory
    {
        private static Dictionary<RuntimeTypeHandle, object> m_WKTypeSerializers;

        // Default static constructor - add Well Known Type (WKT) serializers
        static WKTSerializationFactory()
        {
            // Dictionary with known type serializers
            m_WKTypeSerializers = new Dictionary<RuntimeTypeHandle, object>();

            // Boolean
            RegisterNormalObjectSerializer<Boolean>(Serializer.SerializeBoolean, Serializer.DeserializeBoolean,
                (serializerStorage) => { return new BooleanSerializer(serializerStorage); });

            // Byte
            RegisterNormalObjectSerializer<Byte>(Serializer.SerializeByte, Serializer.DeserializeByte,
                (serializerStorage) => { return new ByteSerializer(serializerStorage); });

            // Char
            RegisterNormalObjectSerializer<Char>(Serializer.SerializeChar, Serializer.DeserializeChar,
                (serializerStorage) => { return new CharSerializer(serializerStorage); });

            // DateTime
            RegisterNormalObjectSerializer<DateTime>(Serializer.SerializeDateTime, Serializer.DeserializeDateTime,
                (serializerStorage) => { return new DateTimeSerializer(serializerStorage); });

            // DateTimeOffset
            RegisterNormalObjectSerializer<DateTimeOffset>(Serializer.SerializeDateTimeOffset, Serializer.DeserializeDateTimeOffset,
                (serializerStorage) => { return new DateTimeOffsetSerializer(serializerStorage); });

            // Decimal
            RegisterNormalObjectSerializer<Decimal>(Serializer.SerializeDecimal, Serializer.DeserializeDecimal,
                (serializerStorage) => { return new DecimalSerializer(serializerStorage); });

            // Double
            RegisterNormalObjectSerializer<Double>(Serializer.SerializeDouble, Serializer.DeserializeDouble,
                (serializerStorage) => { return new DoubleSerializer(serializerStorage); });

            // Guid
            RegisterNormalObjectSerializer<Guid>(Serializer.SerializeGuid, Serializer.DeserializeGuid,
                (serializerStorage) => { return new GuidSerializer(serializerStorage); });

            // Int16
            RegisterNormalObjectSerializer<Int16>(Serializer.SerializeInt16, Serializer.DeserializeInt16,
                (serializerStorage) => { return new Int16Serializer(serializerStorage); });

            // Int32
            RegisterNormalObjectSerializer<Int32>(Serializer.SerializeInt32, Serializer.DeserializeInt32, 
                (serializerStorage) => { return new Int32Serializer(serializerStorage); });

            // Int64
            RegisterNormalObjectSerializer<Int64>(Serializer.SerializeInt64, Serializer.DeserializeInt64,
                (serializerStorage) => { return new Int64Serializer(serializerStorage); });

            // Object
            RegisterNormalObjectSerializer<Object>(Serializer.SerializeObject, Serializer.DeserializeObject,
                (serializerStorage) => { return new ObjectSerializer(serializerStorage); });

            // SByte
            RegisterNormalObjectSerializer<SByte>(Serializer.SerializeSByte, Serializer.DeserializeSByte,
                (serializerStorage) => { return new SByteSerializer(serializerStorage); });

            // Single
            RegisterNormalObjectSerializer<Single>(Serializer.SerializeSingle, Serializer.DeserializeSingle,
                (serializerStorage) => { return new SingleSerializer(serializerStorage); });

            // String
            RegisterValCachedObjectSerializer<string>(Serializer.SerializeString, Serializer.DeserializeString,
                (serializerStorage, disctItf) => { return new StringSerializer(serializerStorage, disctItf); });

            // TimeSpan
            RegisterNormalObjectSerializer<TimeSpan>(Serializer.SerializeTimeSpan, Serializer.DeserializeTimeSpan,
                (serializerStorage) => { return new TimeSpanSerializer(serializerStorage); });

            // UInt16
            RegisterNormalObjectSerializer<UInt16>(Serializer.SerializeUInt16, Serializer.DeserializeUInt16,
                (serializerStorage) => { return new UInt16Serializer(serializerStorage); });

            // UInt32
            RegisterNormalObjectSerializer<UInt32>(Serializer.SerializeUInt32, Serializer.DeserializeUInt32,
                (serializerStorage) => { return new UInt32Serializer(serializerStorage); });

            // UInt64
            RegisterNormalObjectSerializer<UInt64>(Serializer.SerializeUInt64, Serializer.DeserializeUInt64,
                (serializerStorage) => { return new UInt64Serializer(serializerStorage); });
        }

        // Register serializers that doesn't need caching
        private static void RegisterNormalObjectSerializer<WKType>(
            Func<WKType, byte[]> serFunc, 
            Func<byte[], WKType> deserFunc, 
            Func<ISerializerStorage, IObjectSerializer<WKType>> factoryFunc
            )
        {
            m_WKTypeSerializers.Add(typeof(WKType).TypeHandle, new NormalObjectSerializerInfo<WKType>()
            {
                SimpleSerializerFunc = serFunc,
                SimpleDeserializerFunc = deserFunc,
                FactoryFunc = factoryFunc
            });
        }

        // Register serializer that uses Val type caching
        private static void RegisterValCachedObjectSerializer<WKType>(
            Func<WKType, byte[]> serFunc,
            Func<byte[], WKType> deserFunc,
            Func<ISerializerStorage, IValueTypeObjectsDictionary, ICachedObjectSerializer<WKType>> factoryFunc
            )
        {
            m_WKTypeSerializers.Add(typeof(WKType).TypeHandle, new CachedValObjectSerializerInfo<WKType>()
            {
                SimpleSerializerFunc = serFunc,
                SimpleDeserializerFunc = deserFunc,
                FactoryFunc = factoryFunc
            });
        }

        // Register serializer that uses Ref type caching
        private static void RegisterRefCachedObjectSerializer<WKType>(
            Func<WKType, byte[]> serFunc,
            Func<byte[], WKType> deserFunc,
            Func<ISerializerStorage, IRefTypeObjectsDictionary, ICachedObjectSerializer<WKType>> factoryFunc
            )
        {
            m_WKTypeSerializers.Add(typeof(WKType).TypeHandle, new CachedRefObjectSerializerInfo<WKType>()
            {
                SimpleSerializerFunc = serFunc,
                SimpleDeserializerFunc = deserFunc,
                FactoryFunc = factoryFunc
            });
        }

        public static object GetSerializersForType(RuntimeTypeHandle typeHandle)
        {
            if (m_WKTypeSerializers.ContainsKey(typeHandle))
                return m_WKTypeSerializers[typeHandle];

            return null;
        }
    }
}

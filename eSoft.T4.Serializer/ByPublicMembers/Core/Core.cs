using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using eSoft.T4.Core;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace eSoft.T4.Serializer.ByPublicMembers
{
    public partial class Serializer : TextTransformation
    {
        // Key is type full name, value is type handle
        public Dictionary<string, SerializedTypeDesc> SerializedTypes { get; set; }

        // Default serializer namespace (if not specified)
        public string SerializerNamespace { get; set; }

        // Default serializer namespace (if not specified)
        public string SerializerClassName { get; set; }

        // FileNamePrefix for generated files
        public string FileNamePrefix { get; set; }

        // Should we store generated files in separated real files?
        public bool SplitIntoMultipleFiles { get; set; }

        // Default constructor
        public Serializer()
        {
            SerializedTypes = new Dictionary<string, SerializedTypeDesc>();
            SerializerNamespace = "BitCareSerializer";
            SerializerClassName = "BitCareSerializer";
        }

        // New file serialization
        private void StartNewFile(string fileName)
        {
            Manager.SwitchToFile(FileNamePrefix + "_" + fileName + "_Generated.cs");
        }

        // Main serialization routine
        private void SerializeType(SerializedTypeDesc item)
        {
            // Type embedded serialization
            Embedded_Serialization(item);

            // Type external serialization
            External_Serialization(item);
        }

        // Main serialization generator
        public void GenerateSerializers()
        {
            try
            {
                GatherDerivedTypesForMembers();

                // Serialization code for each registered type
                foreach (SerializedTypeDesc item in SerializedTypes.Values)
                    if (!IsWellKnownType(item))
                        SerializeType(item);

                // Final processing
                Manager.GenerateOutput(SplitIntoMultipleFiles);
            }
            catch (Exception ex)
            {
                Write("Exception:");
                WriteLine(ex.ToString());
            }
        }

        private MemberInfo[] GetSerializableMembers(Type serType)
        {
            // item.Type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
            //MemberInfo[] result = FormatterServices.GetSerializableMembers(serType);

            List<MemberInfo> result =new List<MemberInfo>();
            result.AddRange(serType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy));
            result.AddRange(serType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy));

            return result.ToArray();
        }

        private void GatherDerivedTypesForMembers()
        {
            // For all registered types and for all available fields
            foreach (SerializedTypeDesc item in SerializedTypes.Values)
            {
                MemberInfo[] serializableMembers = (MemberInfo[])GetSerializableMembers(item.Type);

                Array.ForEach(serializableMembers, member =>
                    {
                        // Field
                        if (member.MemberType == MemberTypes.Field)
                        {
                            FieldInfo field = (FieldInfo) member;

                            List<SerializedTypeDesc> derivedTypes = new List<SerializedTypeDesc>();
                            derivedTypes.AddRange(GetDerivedTypesForMemberType(field.FieldType));
                            derivedTypes.AddRange(GetDerivedTypesFromAttributes(member).Where(x => !derivedTypes.Contains(x)));

                            // Do we have any derived types for field base type?
                            if (derivedTypes.Count>0)
                            {
                                Type fieldType = field.FieldType;

                                if (fieldType.IsArray)
                                    fieldType = fieldType.GetElementType();

                                item.DerivedTypes.Add(field.Name, new DerivedTypesDesc { BaseType = GetExistingTypeDesc(fieldType), DerivedTypes = derivedTypes.ToArray() });
                            }
                        }

                        // Property
                        if (member.MemberType == MemberTypes.Property)
                        {
                            PropertyInfo property = (PropertyInfo)member;

                            List<SerializedTypeDesc> derivedTypes = new List<SerializedTypeDesc>();
                            derivedTypes.AddRange(GetDerivedTypesForMemberType(property.PropertyType));
                            derivedTypes.AddRange(GetDerivedTypesFromAttributes(member).Where(x => !derivedTypes.Contains(x)));

                            // Do we have any derived types for field base type?
                            if (derivedTypes.Count>0)
                            {
                                Type propertyType = property.PropertyType;

                                if (propertyType.IsArray)
                                    propertyType = propertyType.GetElementType();

                                item.DerivedTypes.Add(property.Name, new DerivedTypesDesc { BaseType = GetExistingTypeDesc(propertyType), DerivedTypes = derivedTypes.ToArray() });
                            }
                        }
                    });
            }
        }

        private List<SerializedTypeDesc> GetDerivedTypesForMemberType(Type memberType)
        {
            List<SerializedTypeDesc> derivedTypes = new List<SerializedTypeDesc>();

            if (memberType.IsArray)
                memberType = memberType.GetElementType();

            if (memberType != typeof(Object))
            {
                // For all registered types
                foreach (SerializedTypeDesc item in SerializedTypes.Values)
                    if (item.Type.IsSubclassOf(memberType))
                        derivedTypes.Add(item);
            }

            return derivedTypes;
        }

        private List<SerializedTypeDesc> GetDerivedTypesFromAttributes(MemberInfo member)
        {
            List<SerializedTypeDesc> derivedTypes = new List<SerializedTypeDesc>();

            // XmlElementAttribute
            if (member.IsDefined(typeof(XmlElementAttribute), true))
            {
                XmlElementAttribute[] xmlElemAttributes = (XmlElementAttribute[])Attribute.GetCustomAttributes(member, typeof(XmlElementAttribute));

                foreach (XmlElementAttribute attr in xmlElemAttributes)
                {
                    if (attr.Type != null)
                    {
                        SerializedTypeDesc attrSerTypeDesc = new SerializedTypeDesc { TypeHandle = attr.Type.TypeHandle };
                        if (!derivedTypes.Contains(attrSerTypeDesc))
                            derivedTypes.Add(attrSerTypeDesc);
                    }
                }
            }

            return derivedTypes;
        }

        // We don't generate serialization code for well known types
        private bool IsWellKnownType(SerializedTypeDesc item)
        {
            Type itemType = item.Type;
            return IsWellKnownType(itemType);
        }

        private bool IsWellKnownType(Type itemType)
        {
            if (
                itemType == typeof(Boolean) ||
                itemType == typeof(Byte) ||
                itemType == typeof(Char) ||
                itemType == typeof(DateTime) ||
                itemType == typeof(DateTimeOffset) ||
                itemType == typeof(Decimal) ||
                itemType == typeof(Double) ||
                itemType == typeof(Guid) ||
                itemType == typeof(Int16) ||
                itemType == typeof(Int32) ||
                itemType == typeof(Int64) ||
                itemType == typeof(Object) ||
                itemType == typeof(SByte) ||
                itemType == typeof(Single) ||
                itemType == typeof(String) ||
                itemType == typeof(TimeSpan) ||
                itemType == typeof(UInt16) ||
                itemType == typeof(UInt32) ||
                itemType == typeof(UInt64)
                )
                return true;

            return false;
        }

        private SerializedTypeDesc GetExistingTypeDesc(Type type)
        {
            SerializedTypeDesc typeDesc = new SerializedTypeDesc { TypeHandle = type.TypeHandle };

            if (SerializedTypes.ContainsKey(typeDesc.SafeFullName))
                return SerializedTypes[typeDesc.SafeFullName];

            SerializedTypes.Add(typeDesc.SafeFullName, typeDesc);
            return typeDesc;
        }

        private Type[] GetTypesFromSerializationAttributesOfTypeMembers(Type serType)
        {
            // Result
            List<Type> result = new List<Type>();

            // Temporary storage
            List<MemberInfo> elementsToInspect = new List<MemberInfo>();
            elementsToInspect.AddRange(GetSerializableMembers(serType));

            //// Fields
            //elementsToInspect.AddRange(serType.GetFields(BindingFlags.Public | BindingFlags.Static));

            //// Properties
            //elementsToInspect.AddRange(serType.GetProperties( BindingFlags.Public | BindingFlags.Static));

            // Inspection
            elementsToInspect.ForEach(elem =>
            {
                XmlElementAttribute[] xmlElemAttributes = (XmlElementAttribute[])Attribute.GetCustomAttributes(elem, typeof(XmlElementAttribute));
                foreach (XmlElementAttribute attr in xmlElemAttributes)
                    if (attr.Type != null)
                        result.Add(attr.Type);
            });

            return result.ToArray();
        }

        // Walks through all the fields and gather types for subtype also
        private void GatherAllUsedTypesForType(Type type)
        {
            // Preprocess full name only
            SerializedTypeDesc typeDesc = new SerializedTypeDesc { TypeHandle = type.TypeHandle };

            // We analyse type once only ...
            if (SerializedTypes.ContainsKey(typeDesc.SafeFullName))
                return;

            // Type registration
            typeDesc = GetExistingTypeDesc(type);

            if (IsWellKnownType(type))
                return;

            // Member fields inspection
            MemberInfo[] serializableMembers = (MemberInfo[])GetSerializableMembers(type);
            Array.ForEach(serializableMembers, member =>
                {
                    // Field
                    if (member.MemberType == MemberTypes.Field)
                    {
                        FieldInfo field = (FieldInfo)member;
                        GatherAllUsedTypesForType(field.FieldType);

                        if (field.FieldType.IsArray)
                            GatherAllUsedTypesForType(field.FieldType.GetElementType());
                    }

                    // Property
                    if (member.MemberType==MemberTypes.Property)
                    {
                        PropertyInfo property = (PropertyInfo)member;
                        GatherAllUsedTypesForType(property.PropertyType);

                        if (property.PropertyType.IsArray)
                            GatherAllUsedTypesForType(property.PropertyType.GetElementType());
                    }
                });

            // Does it have any serialization attributes?
            Type[] otherTypes = GetTypesFromSerializationAttributesOfTypeMembers(type);
            Array.ForEach(otherTypes, otherType =>
            {
                GatherAllUsedTypesForType(otherType);

                if (otherType.IsArray)
                    GatherAllUsedTypesForType(otherType.GetElementType());
            });
        }

        public void RegisterTypeToSerialize(Type typeToSerialize)
        {
            GatherAllUsedTypesForType(typeToSerialize); // Look for used types
        }
    }
}

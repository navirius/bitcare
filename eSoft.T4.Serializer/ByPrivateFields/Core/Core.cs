using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using eSoft.T4.Core;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace eSoft.T4.Serializer.ByPrivateFields
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
            Manager.SwitchToFile(FileNamePrefix+"_"+fileName + "_Generated.cs");
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
                GatherDerivedTypesForFields();

                // Serialization code for each registered type
                foreach (SerializedTypeDesc item in SerializedTypes.Values)
                    if (!IsWellKnownType(item))
                        SerializeType(item);

                // Serializers initialization - beginning of the doc
                StartNewFile(SerializerClassName + "_Initialization");
                ES_InitializationFileHeader();
                
                PushIndent();
                PushIndent();

                foreach (SerializedTypeDesc item in SerializedTypes.Values)
                    if (!IsWellKnownType(item))
                        ES_StaticInitRegister(item);

                PopIndent();
                PopIndent();

                // Serializers initialization - end of the doc
                ES_InitializationFileFooter();
                Manager.RestorePreviousBlock();

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

            MemberInfo[] result = FormatterServices.GetSerializableMembers(serType);
            return result;
        }

        private void GatherDerivedTypesForFields()
        {
            // For all registered types and for all available fields
            foreach (SerializedTypeDesc item in SerializedTypes.Values)
            {
                FieldInfo[] serializableMembers = (FieldInfo[]) GetSerializableMembers(item.Type);

                Array.ForEach(serializableMembers, field =>
                    {
                        SerializedTypeDesc[] derivedTypes = GetDerivedTypesForFieldType(field);

                        // Do we have any derived types for field base type?
                        if (derivedTypes != null)
                        {
                            Type fieldType = field.FieldType;

                            if (fieldType.IsArray)
                                fieldType = fieldType.GetElementType();

                            item.DerivedTypes.Add(field.Name, new DerivedTypesDesc { BaseType = GetExistingTypeDesc(fieldType), DerivedTypes = derivedTypes });
                        }
                    });
            }
        }

        private void GetDerivedTypesFromPropertyAttributes(List<SerializedTypeDesc> derivedTypes, Type baseType)
        {
            // Unknown derived types
            List<SerializedTypeDesc> typesToAdd = new List<SerializedTypeDesc>();

            // For all registered types and for all available fields
            foreach (SerializedTypeDesc item in SerializedTypes.Values)
            {
                List<MemberInfo> elementsToInspect=new List<MemberInfo>();

                // Fields
                elementsToInspect.AddRange(item.Type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy));

                // Properties
                elementsToInspect.AddRange(item.Type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy));

                // Inspection
                elementsToInspect.ForEach(elem =>
                    {
                        // XmlElementAttribute
                        if (elem.IsDefined(typeof(XmlElementAttribute),true) )
                        {
                            XmlElementAttribute[] xmlElemAttributes = (XmlElementAttribute[])Attribute.GetCustomAttributes(elem, typeof(XmlElementAttribute));

                            // Is it FieldInfo?
                            if (elem.MemberType == MemberTypes.Field)
                            {
                                FieldInfo fieldInfo = (FieldInfo)elem;
                                Type fieldType = fieldInfo.FieldType;

                                if (fieldType.IsArray)
                                    fieldType = fieldType.GetElementType();

                                if (fieldType == baseType)
                                    foreach (XmlElementAttribute attr in xmlElemAttributes)
                                    {
                                        if (attr.Type != null)
                                        {
                                            SerializedTypeDesc attrSerTypeDesc = new SerializedTypeDesc { TypeHandle = attr.Type.TypeHandle };
                                            if (!derivedTypes.Contains(attrSerTypeDesc))
                                                typesToAdd.Add(attrSerTypeDesc);
                                        }
                                    }
                            }

                            // Is it PropertyInfo?
                            if (elem.MemberType == MemberTypes.Property)
                            {
                                PropertyInfo propertyInfo = (PropertyInfo)elem;
                                Type propType = propertyInfo.PropertyType;

                                if (propType.IsArray)
                                    propType = propType.GetElementType();

                                if (propType == baseType)
                                    foreach (XmlElementAttribute attr in xmlElemAttributes)
                                    {
                                        if (attr.Type != null)
                                        {
                                            SerializedTypeDesc attrSerTypeDesc = new SerializedTypeDesc { TypeHandle = attr.Type.TypeHandle };
                                            if (!derivedTypes.Contains(attrSerTypeDesc))
                                                typesToAdd.Add(attrSerTypeDesc);
                                        }
                                    }
                            }
                        }
                    });
            }

            // Add new types
            typesToAdd.ForEach(elem => { GetExistingTypeDesc(elem.Type); });
            typesToAdd.ForEach(elem => { if (!derivedTypes.Contains(elem)) derivedTypes.Add(GetExistingTypeDesc(elem.Type)); });
        }

        private SerializedTypeDesc[] GetDerivedTypesForFieldType(FieldInfo field)
        {
            List<SerializedTypeDesc> derivedTypes = new List<SerializedTypeDesc>();
            Type fieldElemType = field.FieldType;

            if (field.FieldType.IsArray) 
                fieldElemType = fieldElemType.GetElementType();

            if (fieldElemType != typeof(Object))
            {
                // For all registered types
                foreach (SerializedTypeDesc item in SerializedTypes.Values)
                    if (item.Type.IsSubclassOf(fieldElemType))
                        derivedTypes.Add(item);
            }

            // Add assignable types based on serialization attributes applied on properties
            GetDerivedTypesFromPropertyAttributes(derivedTypes, fieldElemType);

            // If we have any ...
            if (derivedTypes.Count > 0)
                return derivedTypes.ToArray();

            return null;
        }

        // We don't generate serialization code for well known types
        private bool IsWellKnownType(SerializedTypeDesc item)
        {
            Type itemType = item.Type;

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

            // Fields
            elementsToInspect.AddRange(serType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy));

            // Properties
            elementsToInspect.AddRange(serType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy));

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

            // Member fields inspection
            FieldInfo[] serializableMembers = (FieldInfo[])GetSerializableMembers(type);
            Array.ForEach(serializableMembers, field =>
                {
                    GatherAllUsedTypesForType(field.FieldType);

                    if (field.FieldType.IsArray)
                        GatherAllUsedTypesForType(field.FieldType.GetElementType());
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

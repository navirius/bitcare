using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using eSoft.T4.Core;

namespace eSoft.T4.Serializer.ByPrivateFields
{
    public partial class Serializer : TextTransformation
    {
        private void Embedded_Serialization(SerializedTypeDesc item)
        {
            // We don't generate serializer for array types and well know types
            if (item.Type.IsArray || IsWellKnownType(item))
                return;

            // Separate file
            StartNewFile(item.SafeFullName + "_EmbeddedSerializer");
            Embedded_Header(item);

            this.PushIndent();
            this.PushIndent();
            ES_Normal(item);
            this.PopIndent();
            this.PopIndent();

            Embedded_Footer(item);
            Manager.RestorePreviousBlock();
        }

        private string ES_InheritanceChain(SerializedTypeDesc item)
        {
            // Normal object
            string result = "ComplexTypeSerializerBase, ICachedObjectSerializer<" + item.FullName + ">";

            //// Array
            //if (item.Type.IsArray)
            //    result = "ComplexTypeSerializerBase, ICachedObjectSerializer<" + item.FullName + ">"; ;

            return result;
        }

        private void ES_Normal(SerializedTypeDesc item)
        {
            ES_Constructor(item);
            ES_CreateAccessorsForAllFields(item);
            ES_StaticInit(item);
            ES_PolimorphicTypeEnumerations(item);
            ES_PolimorphicSerialization(item);
            ES_Serialization(item);
            ES_Deserialization(item);
        }

        private void ES_PolimorphicSerialization(SerializedTypeDesc item)
        {
            foreach (var fieldName in item.DerivedTypes.Keys)
            {
                DerivedTypesDesc derivedTypesDesc = item.DerivedTypes[fieldName];
                ES_FieldPolimorphicSerialization(fieldName, derivedTypesDesc);
                ES_FieldPolimorphicDeserialization(fieldName, derivedTypesDesc);
            }
        }

        private void ES_CreateAccessorsForAllFields(SerializedTypeDesc item)
        {
            NewLine();
            WriteLine("#region Accessors for all the fields");
            NewLine();

            // Member fields inspection
            FieldInfo[] serializableMembers = (FieldInfo[])GetSerializableMembers(item.Type);
            Array.ForEach(serializableMembers, field =>
                {
                    ES_CreateAccessor4Field(item, field);
                });

            NewLine();
            WriteLine("#endregion");
            NewLine();
        }

        private void ES_PolimorphicTypeEnumerations(SerializedTypeDesc item)
        {
	        foreach(var fieldName in item.DerivedTypes.Keys)
	        {
		        NewLine();
		        DerivedTypesDesc derivedTypesDesc=item.DerivedTypes[fieldName];
                WriteLine("// Possible derived types to store on field " + fieldName + " - base type is " + derivedTypesDesc.BaseType.FullName);
                Write("public enum " + fieldName + "_SerializedTypeId { TypeIs_" + derivedTypesDesc.BaseType.SafeFullName);
		
		        SerializedTypeDesc[] derivedTypes=derivedTypesDesc.DerivedTypes;

                foreach (var derType in derivedTypes)
                    Write(", TypeIs_" + derType.SafeFullName);

                WriteLine(" }");
	        }
        }

        private void ES_SerializeMembers(SerializedTypeDesc item)
        {
            // List of all the members we can serialize
            List<FieldInfo> serializableMembers = new List<FieldInfo>((FieldInfo[])GetSerializableMembers(item.Type));

            // Serialize normal fields
            List<FieldInfo> normalFields = new List<FieldInfo>();
            normalFields.AddRange(serializableMembers.Where(info=>
            {
                // Is it array?
                if (info.FieldType.IsArray)
                    return false;
                
                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return false;

                return true;
            }));

            normalFields.ForEach(field => ES_SerializeNormalMember(item, field));
            

            // Serialize polymorphic fields
            List<FieldInfo> pilimorhicFields = new List<FieldInfo>();
            pilimorhicFields.AddRange(serializableMembers.Where(info =>
            {
                // Is it array?
                if (info.FieldType.IsArray)
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return true;

                return false;
            }));

            pilimorhicFields.ForEach(field => 
            {
                SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(field.FieldType);
                ES_SerializePolimorphicMember(item, field, fieldItemTypeDesc); 
            });

            // Serialize normal array fields
            List<FieldInfo> normalArrayFields = new List<FieldInfo>();
            normalArrayFields.AddRange(serializableMembers.Where(info =>
            {
                // Is itn't array?
                if ( ! info.FieldType.IsArray)
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return false;

                return true;
            }));

            normalArrayFields.ForEach(field =>
            {
                SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(field.FieldType);
                ES_SerializeNormalArrayMember(item, field, fieldItemTypeDesc);
            });

            // Serialize polimorphic arrays
            List<FieldInfo> polimorphicArrayFields = new List<FieldInfo>();
            polimorphicArrayFields.AddRange(serializableMembers.Where(info =>
            {
                // Is itn't array?
                if (!info.FieldType.IsArray)
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return true;

                return false;
            }));

            polimorphicArrayFields.ForEach(field => 
            {
                SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(field.FieldType);
                SerializedTypeDesc elemItemTypeDesc = GetExistingTypeDesc(field.FieldType.GetElementType());
                ES_SerializePolymorphicArrayMember(item, field, fieldItemTypeDesc, elemItemTypeDesc); 
            });
        }

        private void ES_SerializeNormalMember(SerializedTypeDesc item, FieldInfo field)
        {
            SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(field.FieldType);

            if (IsWellKnownType(fieldItemTypeDesc))
                ES_SerializeNormalWKTMember(item, field, fieldItemTypeDesc);
            else
                ES_SerializeNormalObjectMember(item, field, fieldItemTypeDesc);
        }

        private void ES_SerializeNormalArrayMember(SerializedTypeDesc item, FieldInfo field, SerializedTypeDesc fieldItemTypeDesc)
        {
            SerializedTypeDesc elemItemTypeDesc = GetExistingTypeDesc(field.FieldType.GetElementType());

            if (IsWellKnownType(elemItemTypeDesc))
                ES_SerializeNormalWKTArrayMember(item, field, fieldItemTypeDesc, elemItemTypeDesc);
            else
                ES_SerializeNormalObjectArrayMember(item, field, fieldItemTypeDesc, elemItemTypeDesc);
        }

        private void ES_DeserializeMembers(SerializedTypeDesc item)
        {
            // List of all the members we can serialize
            List<FieldInfo> serializableMembers = new List<FieldInfo>((FieldInfo[])GetSerializableMembers(item.Type));

            // Serialize normal fields
            List<FieldInfo> normalFields = new List<FieldInfo>();
            normalFields.AddRange(serializableMembers.Where(info =>
            {
                // Is it array?
                if (info.FieldType.IsArray)
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return false;

                return true;
            }));

            normalFields.ForEach(field => ES_DeserializeNormalMember(item, field));


            // Serialize polymorphic fields
            List<FieldInfo> pilimorhicFields = new List<FieldInfo>();
            pilimorhicFields.AddRange(serializableMembers.Where(info =>
            {
                // Is it array?
                if (info.FieldType.IsArray)
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return true;

                return false;
            }));

            pilimorhicFields.ForEach(field =>
            {
                SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(field.FieldType);
                ES_DeserializePolymorphicMember(item, field, fieldItemTypeDesc);
            });

            // Serialize normal array fields
            List<FieldInfo> normalArrayFields = new List<FieldInfo>();
            normalArrayFields.AddRange(serializableMembers.Where(info =>
            {
                // Is itn't array?
                if (!info.FieldType.IsArray)
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return false;

                return true;
            }));

            normalArrayFields.ForEach(field =>
            {
                SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(field.FieldType);
                ES_DeserializeNormalArrayMember(item, field, fieldItemTypeDesc);
            });

            // Serialize polimorphic arrays
            List<FieldInfo> polimorphicArrayFields = new List<FieldInfo>();
            polimorphicArrayFields.AddRange(serializableMembers.Where(info =>
            {
                // Is itn't array?
                if (!info.FieldType.IsArray)
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return true;

                return false;
            }));

            polimorphicArrayFields.ForEach(field =>
            {
                SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(field.FieldType);
                SerializedTypeDesc elemItemTypeDesc = GetExistingTypeDesc(field.FieldType.GetElementType());
                ES_DeserializePolymorphicArrayMember(item, field, fieldItemTypeDesc, elemItemTypeDesc);
            });
        }

        private void ES_DeserializeNormalMember(SerializedTypeDesc item, FieldInfo field)
        {
            SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(field.FieldType);

            if (IsWellKnownType(fieldItemTypeDesc))
                ES_DeserializeNormalWKTMember(item, field, fieldItemTypeDesc);
            else
                ES_DeserializeNormalObjectMember(item, field, fieldItemTypeDesc);
        }

        private void ES_DeserializeNormalArrayMember(SerializedTypeDesc item, FieldInfo field, SerializedTypeDesc fieldItemTypeDesc)
        {
            SerializedTypeDesc elemItemTypeDesc = GetExistingTypeDesc(field.FieldType.GetElementType());

            if (IsWellKnownType(elemItemTypeDesc))
                ES_DeserializeNormalWKTArrayMember(item, field, fieldItemTypeDesc, elemItemTypeDesc);
            else
                ES_DeserializeNormalObjectArrayMember(item, field, fieldItemTypeDesc, elemItemTypeDesc);
        }
    }
}

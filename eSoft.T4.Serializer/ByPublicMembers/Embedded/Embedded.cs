using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using eSoft.T4.Core;

namespace eSoft.T4.Serializer.ByPublicMembers
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

        private bool MemberTypeIsArray(MemberInfo info)
        {
            // Field
            if (info.MemberType == MemberTypes.Field)
            {
                if (((FieldInfo)info).FieldType.IsArray)
                    return true;

                return false;
            }

            // Property
            if (((PropertyInfo)info).PropertyType.IsArray)
                return true;

            return false;
        }

        private Type GetMemberType(MemberInfo info)
        {
            if (info.MemberType == MemberTypes.Field)
                return ((FieldInfo)info).FieldType;

            return ((PropertyInfo)info).PropertyType;
        }

        private void ES_SerializeMembers(SerializedTypeDesc item)
        {
            if (item.Name == "ePCR_DocumentPatientInfoPatientHistoryAllergies")
            {
                int k = 0;
            }

            // List of all the members we can serialize
            List<MemberInfo> serializableMembers = new List<MemberInfo>((MemberInfo[])GetSerializableMembers(item.Type));

            // Serialize normal fields
            List<MemberInfo> normalMembers = new List<MemberInfo>();
            normalMembers.AddRange(serializableMembers.Where(info =>
            {
                // Is it array?
                if (MemberTypeIsArray(info))
                    return false;
                
                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return false;

                return true;
            }));

            normalMembers.ForEach(member => ES_SerializeNormalMember(item, member));

            // Serialize polymorphic fields
            List<MemberInfo> pilimorhicMembers = new List<MemberInfo>();
            pilimorhicMembers.AddRange(serializableMembers.Where(info =>
            {
                // Is it array?
                if (MemberTypeIsArray(info))
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return true;

                return false;
            }));

            pilimorhicMembers.ForEach(member => 
            {
                SerializedTypeDesc memberItemTypeDesc = GetExistingTypeDesc(GetMemberType(member));
                ES_SerializePolimorphicMember(item, member, memberItemTypeDesc); 
            });

            // Serialize normal array fields
            List<MemberInfo> normalArrayMembers = new List<MemberInfo>();
            normalArrayMembers.AddRange(serializableMembers.Where(info =>
            {
                // Is itn't array?
                if ( ! MemberTypeIsArray(info))
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return false;

                return true;
            }));

            normalArrayMembers.ForEach(member =>
            {
                SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(GetMemberType(member));
                ES_SerializeNormalArrayMember(item, member, fieldItemTypeDesc);
            });

            // Serialize polimorphic arrays
            List<MemberInfo> polimorphicArrayFields = new List<MemberInfo>();
            polimorphicArrayFields.AddRange(serializableMembers.Where(info =>
            {
                // Is itn't array?
                if ( ! MemberTypeIsArray(info))
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return true;

                return false;
            }));

            polimorphicArrayFields.ForEach(member => 
            {
                SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(GetMemberType(member));
                SerializedTypeDesc elemItemTypeDesc = GetExistingTypeDesc(GetMemberType(member).GetElementType());
                ES_SerializePolymorphicArrayMember(item, member, fieldItemTypeDesc, elemItemTypeDesc); 
            });
        }

        private void ES_SerializeNormalMember(SerializedTypeDesc item, MemberInfo member)
        {
            SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(GetMemberType(member));

            if (IsWellKnownType(fieldItemTypeDesc))
                ES_SerializeNormalWKTMember(item, member, fieldItemTypeDesc);
            else
                ES_SerializeNormalObjectMember(item, member, fieldItemTypeDesc);
        }

        private void ES_SerializeNormalArrayMember(SerializedTypeDesc item, MemberInfo member, SerializedTypeDesc fieldItemTypeDesc)
        {
            SerializedTypeDesc elemItemTypeDesc = GetExistingTypeDesc(GetMemberType(member).GetElementType());

            if (IsWellKnownType(elemItemTypeDesc))
                ES_SerializeNormalWKTArrayMember(item, member, fieldItemTypeDesc, elemItemTypeDesc);
            else
                ES_SerializeNormalObjectArrayMember(item, member, fieldItemTypeDesc, elemItemTypeDesc);
        }

        private void ES_DeserializeMembers(SerializedTypeDesc item)
        {
            // List of all the members we can serialize
            List<MemberInfo> serializableMembers = new List<MemberInfo>((MemberInfo[])GetSerializableMembers(item.Type));

            // Serialize normal fields
            List<MemberInfo> normalFields = new List<MemberInfo>();
            normalFields.AddRange(serializableMembers.Where(info =>
            {
                // Is it array?
                if (GetMemberType(info).IsArray)
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return false;

                return true;
            }));

            normalFields.ForEach(member => ES_DeserializeNormalMember(item, member));


            // Serialize polymorphic fields
            List<MemberInfo> pilimorhicFields = new List<MemberInfo>();
            pilimorhicFields.AddRange(serializableMembers.Where(info =>
            {
                // Is it array?
                if (GetMemberType(info).IsArray)
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return true;

                return false;
            }));

            pilimorhicFields.ForEach(field =>
            {
                SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(GetMemberType(field));
                ES_DeserializePolymorphicMember(item, field, fieldItemTypeDesc);
            });

            // Serialize normal array fields
            List<MemberInfo> normalArrayFields = new List<MemberInfo>();
            normalArrayFields.AddRange(serializableMembers.Where(info =>
            {
                // Is itn't array?
                if (!GetMemberType(info).IsArray)
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return false;

                return true;
            }));

            normalArrayFields.ForEach(field =>
            {
                SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(GetMemberType(field));
                ES_DeserializeNormalArrayMember(item, field, fieldItemTypeDesc);
            });

            // Serialize polimorphic arrays
            List<MemberInfo> polimorphicArrayFields = new List<MemberInfo>();
            polimorphicArrayFields.AddRange(serializableMembers.Where(info =>
            {
                // Is itn't array?
                if (!GetMemberType(info).IsArray)
                    return false;

                // Is it polymorphic type field?
                if (item.DerivedTypes.ContainsKey(info.Name))
                    return true;

                return false;
            }));

            polimorphicArrayFields.ForEach(field =>
            {
                SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(GetMemberType(field));
                SerializedTypeDesc elemItemTypeDesc = GetExistingTypeDesc(GetMemberType(field).GetElementType());
                ES_DeserializePolymorphicArrayMember(item, field, fieldItemTypeDesc, elemItemTypeDesc);
            });
        }

        private void ES_DeserializeNormalMember(SerializedTypeDesc item, MemberInfo field)
        {
            SerializedTypeDesc fieldItemTypeDesc = GetExistingTypeDesc(GetMemberType(field));

            if (IsWellKnownType(fieldItemTypeDesc))
                ES_DeserializeNormalWKTMember(item, field, fieldItemTypeDesc);
            else
                ES_DeserializeNormalObjectMember(item, field, fieldItemTypeDesc);
        }

        private void ES_DeserializeNormalArrayMember(SerializedTypeDesc item, MemberInfo field, SerializedTypeDesc fieldItemTypeDesc)
        {
            SerializedTypeDesc elemItemTypeDesc = GetExistingTypeDesc(GetMemberType(field).GetElementType());

            if (IsWellKnownType(elemItemTypeDesc))
                ES_DeserializeNormalWKTArrayMember(item, field, fieldItemTypeDesc, elemItemTypeDesc);
            else
                ES_DeserializeNormalObjectArrayMember(item, field, fieldItemTypeDesc, elemItemTypeDesc);
        }
    }
}

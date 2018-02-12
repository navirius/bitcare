// -----------------------------------------------------------------------------------------------------------------------------
// This is file generated for type PerfTests.Classes.AzureEntityFramework.Address - Don't modify it manually !!!
// -----------------------------------------------------------------------------------------------------------------------------
using System;
using System.Reflection;
using System.Reflection.Emit;
using eSoft.Serializer;
using eSoft.Serializer.Exceptions;
using eSoft.Serializer.TypeSerializers;
using eSoft.Serializer.TypeSerializers.WellKnown;
using eSoft.Serializer.ObjectDictionaries.RefType;
using eSoft.Serializer.ObjectDictionaries.ValType;
using eSoft.Serializer.Infrastructure.PolimorphicType;
using eSoft.Serializer.Infrastructure.Accessors;

namespace BitCareSerializer.ByFields.v1 
{
    public partial class  PerfTests_Classes_AzureEntityFramework_AddressSerializer : ComplexTypeSerializerBase, ICachedObjectSerializer<PerfTests.Classes.AzureEntityFramework.Address>
    {
        // Constructor
        public PerfTests_Classes_AzureEntityFramework_AddressSerializer(ISerializerStorage serializerStorage, IRefTypeObjectsDictionary refObjectsCache, IValueTypeObjectsDictionary valObjectsCache) 
        			: base(serializerStorage, refObjectsCache, valObjectsCache) { }

        #region Accessors for all the fields

        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.String > s_Accessor4_StreetName_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.String > s_Accessor4_Building_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.String > s_Accessor4_City_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.String > s_Accessor4_PostCode_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.String > s_Accessor4_Country_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.Guid > s_Accessor4_ExternalId1_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.Guid > s_Accessor4_ExternalId2_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.Guid > s_Accessor4_ExternalId3_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.Guid > s_Accessor4_ExternalId4_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.Guid > s_Accessor4_ExternalId5_k__BackingField;

        #endregion

        // Main initialization method
        public static void Init()
        {
            s_Accessor4_StreetName_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.String >("<StreetName>k__BackingField");
            s_Accessor4_Building_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.String >("<Building>k__BackingField");
            s_Accessor4_City_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.String >("<City>k__BackingField");
            s_Accessor4_PostCode_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.String >("<PostCode>k__BackingField");
            s_Accessor4_Country_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.String >("<Country>k__BackingField");
            s_Accessor4_ExternalId1_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.Guid >("<ExternalId1>k__BackingField");
            s_Accessor4_ExternalId2_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.Guid >("<ExternalId2>k__BackingField");
            s_Accessor4_ExternalId3_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.Guid >("<ExternalId3>k__BackingField");
            s_Accessor4_ExternalId4_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.Guid >("<ExternalId4>k__BackingField");
            s_Accessor4_ExternalId5_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Address, System.Guid >("<ExternalId5>k__BackingField");
        }
        
        // Serialization
        public void Serialize(PerfTests.Classes.AzureEntityFramework.Address serializedObject)
        {
            // Do we have to store full data (when it's not null or cached object)?
            if ( ! ShouldStoreFullData(serializedObject))
                return;
        
        	// <StreetName>k__BackingField - WellKnownType object
        	System.String _StreetName_k__BackingFieldValue = s_Accessor4_StreetName_k__BackingField.Get(serializedObject);
        	WKTSerializers.String.Serialize(_StreetName_k__BackingFieldValue);
        
        	// <Building>k__BackingField - WellKnownType object
        	System.String _Building_k__BackingFieldValue = s_Accessor4_Building_k__BackingField.Get(serializedObject);
        	WKTSerializers.String.Serialize(_Building_k__BackingFieldValue);
        
        	// <City>k__BackingField - WellKnownType object
        	System.String _City_k__BackingFieldValue = s_Accessor4_City_k__BackingField.Get(serializedObject);
        	WKTSerializers.String.Serialize(_City_k__BackingFieldValue);
        
        	// <PostCode>k__BackingField - WellKnownType object
        	System.String _PostCode_k__BackingFieldValue = s_Accessor4_PostCode_k__BackingField.Get(serializedObject);
        	WKTSerializers.String.Serialize(_PostCode_k__BackingFieldValue);
        
        	// <Country>k__BackingField - WellKnownType object
        	System.String _Country_k__BackingFieldValue = s_Accessor4_Country_k__BackingField.Get(serializedObject);
        	WKTSerializers.String.Serialize(_Country_k__BackingFieldValue);
        
        	// <ExternalId1>k__BackingField - WellKnownType object
        	System.Guid _ExternalId1_k__BackingFieldValue = s_Accessor4_ExternalId1_k__BackingField.Get(serializedObject);
        	WKTSerializers.Guid.Serialize(_ExternalId1_k__BackingFieldValue);
        
        	// <ExternalId2>k__BackingField - WellKnownType object
        	System.Guid _ExternalId2_k__BackingFieldValue = s_Accessor4_ExternalId2_k__BackingField.Get(serializedObject);
        	WKTSerializers.Guid.Serialize(_ExternalId2_k__BackingFieldValue);
        
        	// <ExternalId3>k__BackingField - WellKnownType object
        	System.Guid _ExternalId3_k__BackingFieldValue = s_Accessor4_ExternalId3_k__BackingField.Get(serializedObject);
        	WKTSerializers.Guid.Serialize(_ExternalId3_k__BackingFieldValue);
        
        	// <ExternalId4>k__BackingField - WellKnownType object
        	System.Guid _ExternalId4_k__BackingFieldValue = s_Accessor4_ExternalId4_k__BackingField.Get(serializedObject);
        	WKTSerializers.Guid.Serialize(_ExternalId4_k__BackingFieldValue);
        
        	// <ExternalId5>k__BackingField - WellKnownType object
        	System.Guid _ExternalId5_k__BackingFieldValue = s_Accessor4_ExternalId5_k__BackingField.Get(serializedObject);
        	WKTSerializers.Guid.Serialize(_ExternalId5_k__BackingFieldValue);
        }
        
        public PerfTests.Classes.AzureEntityFramework.Address Deserialize()
        {
            // Result object
            PerfTests.Classes.AzureEntityFramework.Address result = new PerfTests.Classes.AzureEntityFramework.Address();
        
            // Do we have to load full data (when it's not null or cached object)?
            if ( ! ShouldLoadFullData(ref result))
                return result;
        
        	// <StreetName>k__BackingField - WKT Deserialization
        	String _StreetName_k__BackingFieldValue = WKTSerializers.String.Deserialize();
        	s_Accessor4_StreetName_k__BackingField.Set(result, _StreetName_k__BackingFieldValue);
        
        	// <Building>k__BackingField - WKT Deserialization
        	String _Building_k__BackingFieldValue = WKTSerializers.String.Deserialize();
        	s_Accessor4_Building_k__BackingField.Set(result, _Building_k__BackingFieldValue);
        
        	// <City>k__BackingField - WKT Deserialization
        	String _City_k__BackingFieldValue = WKTSerializers.String.Deserialize();
        	s_Accessor4_City_k__BackingField.Set(result, _City_k__BackingFieldValue);
        
        	// <PostCode>k__BackingField - WKT Deserialization
        	String _PostCode_k__BackingFieldValue = WKTSerializers.String.Deserialize();
        	s_Accessor4_PostCode_k__BackingField.Set(result, _PostCode_k__BackingFieldValue);
        
        	// <Country>k__BackingField - WKT Deserialization
        	String _Country_k__BackingFieldValue = WKTSerializers.String.Deserialize();
        	s_Accessor4_Country_k__BackingField.Set(result, _Country_k__BackingFieldValue);
        
        	// <ExternalId1>k__BackingField - WKT Deserialization
        	Guid _ExternalId1_k__BackingFieldValue = WKTSerializers.Guid.Deserialize();
        	s_Accessor4_ExternalId1_k__BackingField.Set(result, _ExternalId1_k__BackingFieldValue);
        
        	// <ExternalId2>k__BackingField - WKT Deserialization
        	Guid _ExternalId2_k__BackingFieldValue = WKTSerializers.Guid.Deserialize();
        	s_Accessor4_ExternalId2_k__BackingField.Set(result, _ExternalId2_k__BackingFieldValue);
        
        	// <ExternalId3>k__BackingField - WKT Deserialization
        	Guid _ExternalId3_k__BackingFieldValue = WKTSerializers.Guid.Deserialize();
        	s_Accessor4_ExternalId3_k__BackingField.Set(result, _ExternalId3_k__BackingFieldValue);
        
        	// <ExternalId4>k__BackingField - WKT Deserialization
        	Guid _ExternalId4_k__BackingFieldValue = WKTSerializers.Guid.Deserialize();
        	s_Accessor4_ExternalId4_k__BackingField.Set(result, _ExternalId4_k__BackingFieldValue);
        
        	// <ExternalId5>k__BackingField - WKT Deserialization
        	Guid _ExternalId5_k__BackingFieldValue = WKTSerializers.Guid.Deserialize();
        	s_Accessor4_ExternalId5_k__BackingField.Set(result, _ExternalId5_k__BackingFieldValue);
        
            // Update cache when necessary
            UpdateRefObjectsCache(result);
        
            // Return result
            return result;
        }
	}
}

// -----------------------------------------------------------------------------------------------------------------------------
// End of file definition for type PerfTests.Classes.AzureEntityFramework.Address
// -----------------------------------------------------------------------------------------------------------------------------


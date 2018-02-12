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

namespace BitCareSerializer.ByPubMembers.v1 
{
    public partial class  PerfTests_Classes_AzureEntityFramework_AddressSerializer : ComplexTypeSerializerBase, ICachedObjectSerializer<PerfTests.Classes.AzureEntityFramework.Address>
    {
        // Constructor
        public PerfTests_Classes_AzureEntityFramework_AddressSerializer(ISerializerStorage serializerStorage, IRefTypeObjectsDictionary refObjectsCache, IValueTypeObjectsDictionary valObjectsCache) 
        			: base(serializerStorage, refObjectsCache, valObjectsCache) { }
        
        // Serialization
        public void Serialize(PerfTests.Classes.AzureEntityFramework.Address serializedObject)
        {
            // Do we have to store full data (when it's not null or cached object)?
            if ( ! ShouldStoreFullData(serializedObject))
                return;
        
        	// StreetName - WellKnownType object
        	WKTSerializers.String.Serialize(serializedObject.StreetName);
        
        	// Building - WellKnownType object
        	WKTSerializers.String.Serialize(serializedObject.Building);
        
        	// City - WellKnownType object
        	WKTSerializers.String.Serialize(serializedObject.City);
        
        	// PostCode - WellKnownType object
        	WKTSerializers.String.Serialize(serializedObject.PostCode);
        
        	// Country - WellKnownType object
        	WKTSerializers.String.Serialize(serializedObject.Country);
        
        	// ExternalId1 - WellKnownType object
        	WKTSerializers.Guid.Serialize(serializedObject.ExternalId1);
        
        	// ExternalId2 - WellKnownType object
        	WKTSerializers.Guid.Serialize(serializedObject.ExternalId2);
        
        	// ExternalId3 - WellKnownType object
        	WKTSerializers.Guid.Serialize(serializedObject.ExternalId3);
        
        	// ExternalId4 - WellKnownType object
        	WKTSerializers.Guid.Serialize(serializedObject.ExternalId4);
        
        	// ExternalId5 - WellKnownType object
        	WKTSerializers.Guid.Serialize(serializedObject.ExternalId5);
        }
        
        public PerfTests.Classes.AzureEntityFramework.Address Deserialize()
        {
            // Result object
            PerfTests.Classes.AzureEntityFramework.Address result = new PerfTests.Classes.AzureEntityFramework.Address();
        
            // Do we have to load full data (when it's not null or cached object)?
            if ( ! ShouldLoadFullData(ref result))
                return result;
        
        	// StreetName - WKT Deserialization
        	String StreetNameValue = WKTSerializers.String.Deserialize();
        	result.StreetName = StreetNameValue;
        
        	// Building - WKT Deserialization
        	String BuildingValue = WKTSerializers.String.Deserialize();
        	result.Building = BuildingValue;
        
        	// City - WKT Deserialization
        	String CityValue = WKTSerializers.String.Deserialize();
        	result.City = CityValue;
        
        	// PostCode - WKT Deserialization
        	String PostCodeValue = WKTSerializers.String.Deserialize();
        	result.PostCode = PostCodeValue;
        
        	// Country - WKT Deserialization
        	String CountryValue = WKTSerializers.String.Deserialize();
        	result.Country = CountryValue;
        
        	// ExternalId1 - WKT Deserialization
        	Guid ExternalId1Value = WKTSerializers.Guid.Deserialize();
        	result.ExternalId1 = ExternalId1Value;
        
        	// ExternalId2 - WKT Deserialization
        	Guid ExternalId2Value = WKTSerializers.Guid.Deserialize();
        	result.ExternalId2 = ExternalId2Value;
        
        	// ExternalId3 - WKT Deserialization
        	Guid ExternalId3Value = WKTSerializers.Guid.Deserialize();
        	result.ExternalId3 = ExternalId3Value;
        
        	// ExternalId4 - WKT Deserialization
        	Guid ExternalId4Value = WKTSerializers.Guid.Deserialize();
        	result.ExternalId4 = ExternalId4Value;
        
        	// ExternalId5 - WKT Deserialization
        	Guid ExternalId5Value = WKTSerializers.Guid.Deserialize();
        	result.ExternalId5 = ExternalId5Value;
        
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


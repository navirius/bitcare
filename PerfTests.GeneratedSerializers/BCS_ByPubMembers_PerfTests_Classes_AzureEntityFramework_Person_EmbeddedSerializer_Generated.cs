// -----------------------------------------------------------------------------------------------------------------------------
// This is file generated for type PerfTests.Classes.AzureEntityFramework.Person - Don't modify it manually !!!
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
    public partial class  PerfTests_Classes_AzureEntityFramework_PersonSerializer : ComplexTypeSerializerBase, ICachedObjectSerializer<PerfTests.Classes.AzureEntityFramework.Person>
    {
        // Constructor
        public PerfTests_Classes_AzureEntityFramework_PersonSerializer(ISerializerStorage serializerStorage, IRefTypeObjectsDictionary refObjectsCache, IValueTypeObjectsDictionary valObjectsCache) 
        			: base(serializerStorage, refObjectsCache, valObjectsCache) { }
        
        // Serialization
        public void Serialize(PerfTests.Classes.AzureEntityFramework.Person serializedObject)
        {
            // Do we have to store full data (when it's not null or cached object)?
            if ( ! ShouldStoreFullData(serializedObject))
                return;
        
        	// PersonID - WellKnownType object
        	WKTSerializers.Guid.Serialize(serializedObject.PersonID);
        
        	// FirstName - WellKnownType object
        	WKTSerializers.String.Serialize(serializedObject.FirstName);
        
        	// LastName - WellKnownType object
        	WKTSerializers.String.Serialize(serializedObject.LastName);
        
        	// BirthDate - WellKnownType object
        	WKTSerializers.DateTime.Serialize(serializedObject.BirthDate);
        
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
        
        	// Sallary1 - WellKnownType object
        	WKTSerializers.Decimal.Serialize(serializedObject.Sallary1);
        
        	// Sallary2 - WellKnownType object
        	WKTSerializers.Decimal.Serialize(serializedObject.Sallary2);
        
        	// Sallary3 - WellKnownType object
        	WKTSerializers.Decimal.Serialize(serializedObject.Sallary3);
        
        	// Sallary4 - WellKnownType object
        	WKTSerializers.Decimal.Serialize(serializedObject.Sallary4);
        
        	// Sallary5 - WellKnownType object
        	WKTSerializers.Decimal.Serialize(serializedObject.Sallary5);
        
        	// Result1 - WellKnownType object
        	WKTSerializers.Double.Serialize(serializedObject.Result1);
        
        	// Result2 - WellKnownType object
        	WKTSerializers.Double.Serialize(serializedObject.Result2);
        
        	// Result3 - WellKnownType object
        	WKTSerializers.Double.Serialize(serializedObject.Result3);
        
        	// Result4 - WellKnownType object
        	WKTSerializers.Double.Serialize(serializedObject.Result4);
        
        	// Result5 - WellKnownType object
        	WKTSerializers.Double.Serialize(serializedObject.Result5);
        
        	// Skill1 - WellKnownType object
        	WKTSerializers.String.Serialize(serializedObject.Skill1);
        
        	// Skill2 - WellKnownType object
        	WKTSerializers.String.Serialize(serializedObject.Skill2);
        
        	// Skill3 - WellKnownType object
        	WKTSerializers.String.Serialize(serializedObject.Skill3);
        
        	// Skill4 - WellKnownType object
        	WKTSerializers.String.Serialize(serializedObject.Skill4);
        
        	// Skill5 - WellKnownType object
        	WKTSerializers.String.Serialize(serializedObject.Skill5);
        
        	// KnownAddresses - normal array of PerfTests.Classes.AzureEntityFramework.Address elements
        	new SZRefArraySerializer< PerfTests.Classes.AzureEntityFramework.Address >(SerializerStorage,
        	    elem => { new PerfTests_Classes_AzureEntityFramework_AddressSerializer(SerializerStorage, ObjectCache, ValObjectsCache).Serialize(elem); },
        	    () => new PerfTests_Classes_AzureEntityFramework_AddressSerializer(SerializerStorage, ObjectCache, ValObjectsCache).Deserialize()
        	    ).Serialize(serializedObject.KnownAddresses);
        }
        
        public PerfTests.Classes.AzureEntityFramework.Person Deserialize()
        {
            // Result object
            PerfTests.Classes.AzureEntityFramework.Person result = new PerfTests.Classes.AzureEntityFramework.Person();
        
            // Do we have to load full data (when it's not null or cached object)?
            if ( ! ShouldLoadFullData(ref result))
                return result;
        
        	// PersonID - WKT Deserialization
        	Guid PersonIDValue = WKTSerializers.Guid.Deserialize();
        	result.PersonID = PersonIDValue;
        
        	// FirstName - WKT Deserialization
        	String FirstNameValue = WKTSerializers.String.Deserialize();
        	result.FirstName = FirstNameValue;
        
        	// LastName - WKT Deserialization
        	String LastNameValue = WKTSerializers.String.Deserialize();
        	result.LastName = LastNameValue;
        
        	// BirthDate - WKT Deserialization
        	DateTime BirthDateValue = WKTSerializers.DateTime.Deserialize();
        	result.BirthDate = BirthDateValue;
        
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
        
        	// Sallary1 - WKT Deserialization
        	Decimal Sallary1Value = WKTSerializers.Decimal.Deserialize();
        	result.Sallary1 = Sallary1Value;
        
        	// Sallary2 - WKT Deserialization
        	Decimal Sallary2Value = WKTSerializers.Decimal.Deserialize();
        	result.Sallary2 = Sallary2Value;
        
        	// Sallary3 - WKT Deserialization
        	Decimal Sallary3Value = WKTSerializers.Decimal.Deserialize();
        	result.Sallary3 = Sallary3Value;
        
        	// Sallary4 - WKT Deserialization
        	Decimal Sallary4Value = WKTSerializers.Decimal.Deserialize();
        	result.Sallary4 = Sallary4Value;
        
        	// Sallary5 - WKT Deserialization
        	Decimal Sallary5Value = WKTSerializers.Decimal.Deserialize();
        	result.Sallary5 = Sallary5Value;
        
        	// Result1 - WKT Deserialization
        	Double Result1Value = WKTSerializers.Double.Deserialize();
        	result.Result1 = Result1Value;
        
        	// Result2 - WKT Deserialization
        	Double Result2Value = WKTSerializers.Double.Deserialize();
        	result.Result2 = Result2Value;
        
        	// Result3 - WKT Deserialization
        	Double Result3Value = WKTSerializers.Double.Deserialize();
        	result.Result3 = Result3Value;
        
        	// Result4 - WKT Deserialization
        	Double Result4Value = WKTSerializers.Double.Deserialize();
        	result.Result4 = Result4Value;
        
        	// Result5 - WKT Deserialization
        	Double Result5Value = WKTSerializers.Double.Deserialize();
        	result.Result5 = Result5Value;
        
        	// Skill1 - WKT Deserialization
        	String Skill1Value = WKTSerializers.String.Deserialize();
        	result.Skill1 = Skill1Value;
        
        	// Skill2 - WKT Deserialization
        	String Skill2Value = WKTSerializers.String.Deserialize();
        	result.Skill2 = Skill2Value;
        
        	// Skill3 - WKT Deserialization
        	String Skill3Value = WKTSerializers.String.Deserialize();
        	result.Skill3 = Skill3Value;
        
        	// Skill4 - WKT Deserialization
        	String Skill4Value = WKTSerializers.String.Deserialize();
        	result.Skill4 = Skill4Value;
        
        	// Skill5 - WKT Deserialization
        	String Skill5Value = WKTSerializers.String.Deserialize();
        	result.Skill5 = Skill5Value;
        
        	// KnownAddresses - the array of normal elements - element type is PerfTests.Classes.AzureEntityFramework.Address
            PerfTests.Classes.AzureEntityFramework.Address[] KnownAddressesValue = new SZRefArraySerializer< PerfTests.Classes.AzureEntityFramework.Address >(SerializerStorage,
                elem => { new PerfTests_Classes_AzureEntityFramework_AddressSerializer(SerializerStorage, ObjectCache, ValObjectsCache).Serialize(elem); },
        	    () => new PerfTests_Classes_AzureEntityFramework_AddressSerializer(SerializerStorage, ObjectCache, ValObjectsCache).Deserialize()
                ).Deserialize();
            result.KnownAddresses = KnownAddressesValue;
        
            // Update cache when necessary
            UpdateRefObjectsCache(result);
        
            // Return result
            return result;
        }
	}
}

// -----------------------------------------------------------------------------------------------------------------------------
// End of file definition for type PerfTests.Classes.AzureEntityFramework.Person
// -----------------------------------------------------------------------------------------------------------------------------


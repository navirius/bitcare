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

namespace BitCareSerializer.ByFields.v1 
{
    public partial class  PerfTests_Classes_AzureEntityFramework_PersonSerializer : ComplexTypeSerializerBase, ICachedObjectSerializer<PerfTests.Classes.AzureEntityFramework.Person>
    {
        // Constructor
        public PerfTests_Classes_AzureEntityFramework_PersonSerializer(ISerializerStorage serializerStorage, IRefTypeObjectsDictionary refObjectsCache, IValueTypeObjectsDictionary valObjectsCache) 
        			: base(serializerStorage, refObjectsCache, valObjectsCache) { }

        #region Accessors for all the fields

        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Guid > s_Accessor4_PersonID_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String > s_Accessor4_FirstName_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String > s_Accessor4_LastName_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.DateTime > s_Accessor4_BirthDate_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, PerfTests.Classes.AzureEntityFramework.Address[] > s_Accessor4_KnownAddresses_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Guid > s_Accessor4_ExternalId1_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Guid > s_Accessor4_ExternalId2_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Guid > s_Accessor4_ExternalId3_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Guid > s_Accessor4_ExternalId4_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Guid > s_Accessor4_ExternalId5_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Decimal > s_Accessor4_Sallary1_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Decimal > s_Accessor4_Sallary2_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Decimal > s_Accessor4_Sallary3_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Decimal > s_Accessor4_Sallary4_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Decimal > s_Accessor4_Sallary5_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Double > s_Accessor4_Result1_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Double > s_Accessor4_Result2_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Double > s_Accessor4_Result3_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Double > s_Accessor4_Result4_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Double > s_Accessor4_Result5_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String > s_Accessor4_Skill1_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String > s_Accessor4_Skill2_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String > s_Accessor4_Skill3_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String > s_Accessor4_Skill4_k__BackingField;
        private static FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String > s_Accessor4_Skill5_k__BackingField;

        #endregion

        // Main initialization method
        public static void Init()
        {
            s_Accessor4_PersonID_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Guid >("<PersonID>k__BackingField");
            s_Accessor4_FirstName_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String >("<FirstName>k__BackingField");
            s_Accessor4_LastName_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String >("<LastName>k__BackingField");
            s_Accessor4_BirthDate_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.DateTime >("<BirthDate>k__BackingField");
            s_Accessor4_KnownAddresses_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, PerfTests.Classes.AzureEntityFramework.Address[] >("<KnownAddresses>k__BackingField");
            s_Accessor4_ExternalId1_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Guid >("<ExternalId1>k__BackingField");
            s_Accessor4_ExternalId2_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Guid >("<ExternalId2>k__BackingField");
            s_Accessor4_ExternalId3_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Guid >("<ExternalId3>k__BackingField");
            s_Accessor4_ExternalId4_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Guid >("<ExternalId4>k__BackingField");
            s_Accessor4_ExternalId5_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Guid >("<ExternalId5>k__BackingField");
            s_Accessor4_Sallary1_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Decimal >("<Sallary1>k__BackingField");
            s_Accessor4_Sallary2_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Decimal >("<Sallary2>k__BackingField");
            s_Accessor4_Sallary3_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Decimal >("<Sallary3>k__BackingField");
            s_Accessor4_Sallary4_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Decimal >("<Sallary4>k__BackingField");
            s_Accessor4_Sallary5_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Decimal >("<Sallary5>k__BackingField");
            s_Accessor4_Result1_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Double >("<Result1>k__BackingField");
            s_Accessor4_Result2_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Double >("<Result2>k__BackingField");
            s_Accessor4_Result3_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Double >("<Result3>k__BackingField");
            s_Accessor4_Result4_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Double >("<Result4>k__BackingField");
            s_Accessor4_Result5_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.Double >("<Result5>k__BackingField");
            s_Accessor4_Skill1_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String >("<Skill1>k__BackingField");
            s_Accessor4_Skill2_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String >("<Skill2>k__BackingField");
            s_Accessor4_Skill3_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String >("<Skill3>k__BackingField");
            s_Accessor4_Skill4_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String >("<Skill4>k__BackingField");
            s_Accessor4_Skill5_k__BackingField = new FieldAccessor< PerfTests.Classes.AzureEntityFramework.Person, System.String >("<Skill5>k__BackingField");
        }
        
        // Serialization
        public void Serialize(PerfTests.Classes.AzureEntityFramework.Person serializedObject)
        {
            // Do we have to store full data (when it's not null or cached object)?
            if ( ! ShouldStoreFullData(serializedObject))
                return;
        
        	// <PersonID>k__BackingField - WellKnownType object
        	System.Guid _PersonID_k__BackingFieldValue = s_Accessor4_PersonID_k__BackingField.Get(serializedObject);
        	WKTSerializers.Guid.Serialize(_PersonID_k__BackingFieldValue);
        
        	// <FirstName>k__BackingField - WellKnownType object
        	System.String _FirstName_k__BackingFieldValue = s_Accessor4_FirstName_k__BackingField.Get(serializedObject);
        	WKTSerializers.String.Serialize(_FirstName_k__BackingFieldValue);
        
        	// <LastName>k__BackingField - WellKnownType object
        	System.String _LastName_k__BackingFieldValue = s_Accessor4_LastName_k__BackingField.Get(serializedObject);
        	WKTSerializers.String.Serialize(_LastName_k__BackingFieldValue);
        
        	// <BirthDate>k__BackingField - WellKnownType object
        	System.DateTime _BirthDate_k__BackingFieldValue = s_Accessor4_BirthDate_k__BackingField.Get(serializedObject);
        	WKTSerializers.DateTime.Serialize(_BirthDate_k__BackingFieldValue);
        
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
        
        	// <Sallary1>k__BackingField - WellKnownType object
        	System.Decimal _Sallary1_k__BackingFieldValue = s_Accessor4_Sallary1_k__BackingField.Get(serializedObject);
        	WKTSerializers.Decimal.Serialize(_Sallary1_k__BackingFieldValue);
        
        	// <Sallary2>k__BackingField - WellKnownType object
        	System.Decimal _Sallary2_k__BackingFieldValue = s_Accessor4_Sallary2_k__BackingField.Get(serializedObject);
        	WKTSerializers.Decimal.Serialize(_Sallary2_k__BackingFieldValue);
        
        	// <Sallary3>k__BackingField - WellKnownType object
        	System.Decimal _Sallary3_k__BackingFieldValue = s_Accessor4_Sallary3_k__BackingField.Get(serializedObject);
        	WKTSerializers.Decimal.Serialize(_Sallary3_k__BackingFieldValue);
        
        	// <Sallary4>k__BackingField - WellKnownType object
        	System.Decimal _Sallary4_k__BackingFieldValue = s_Accessor4_Sallary4_k__BackingField.Get(serializedObject);
        	WKTSerializers.Decimal.Serialize(_Sallary4_k__BackingFieldValue);
        
        	// <Sallary5>k__BackingField - WellKnownType object
        	System.Decimal _Sallary5_k__BackingFieldValue = s_Accessor4_Sallary5_k__BackingField.Get(serializedObject);
        	WKTSerializers.Decimal.Serialize(_Sallary5_k__BackingFieldValue);
        
        	// <Result1>k__BackingField - WellKnownType object
        	System.Double _Result1_k__BackingFieldValue = s_Accessor4_Result1_k__BackingField.Get(serializedObject);
        	WKTSerializers.Double.Serialize(_Result1_k__BackingFieldValue);
        
        	// <Result2>k__BackingField - WellKnownType object
        	System.Double _Result2_k__BackingFieldValue = s_Accessor4_Result2_k__BackingField.Get(serializedObject);
        	WKTSerializers.Double.Serialize(_Result2_k__BackingFieldValue);
        
        	// <Result3>k__BackingField - WellKnownType object
        	System.Double _Result3_k__BackingFieldValue = s_Accessor4_Result3_k__BackingField.Get(serializedObject);
        	WKTSerializers.Double.Serialize(_Result3_k__BackingFieldValue);
        
        	// <Result4>k__BackingField - WellKnownType object
        	System.Double _Result4_k__BackingFieldValue = s_Accessor4_Result4_k__BackingField.Get(serializedObject);
        	WKTSerializers.Double.Serialize(_Result4_k__BackingFieldValue);
        
        	// <Result5>k__BackingField - WellKnownType object
        	System.Double _Result5_k__BackingFieldValue = s_Accessor4_Result5_k__BackingField.Get(serializedObject);
        	WKTSerializers.Double.Serialize(_Result5_k__BackingFieldValue);
        
        	// <Skill1>k__BackingField - WellKnownType object
        	System.String _Skill1_k__BackingFieldValue = s_Accessor4_Skill1_k__BackingField.Get(serializedObject);
        	WKTSerializers.String.Serialize(_Skill1_k__BackingFieldValue);
        
        	// <Skill2>k__BackingField - WellKnownType object
        	System.String _Skill2_k__BackingFieldValue = s_Accessor4_Skill2_k__BackingField.Get(serializedObject);
        	WKTSerializers.String.Serialize(_Skill2_k__BackingFieldValue);
        
        	// <Skill3>k__BackingField - WellKnownType object
        	System.String _Skill3_k__BackingFieldValue = s_Accessor4_Skill3_k__BackingField.Get(serializedObject);
        	WKTSerializers.String.Serialize(_Skill3_k__BackingFieldValue);
        
        	// <Skill4>k__BackingField - WellKnownType object
        	System.String _Skill4_k__BackingFieldValue = s_Accessor4_Skill4_k__BackingField.Get(serializedObject);
        	WKTSerializers.String.Serialize(_Skill4_k__BackingFieldValue);
        
        	// <Skill5>k__BackingField - WellKnownType object
        	System.String _Skill5_k__BackingFieldValue = s_Accessor4_Skill5_k__BackingField.Get(serializedObject);
        	WKTSerializers.String.Serialize(_Skill5_k__BackingFieldValue);
        
        	// <KnownAddresses>k__BackingField - normal array of PerfTests.Classes.AzureEntityFramework.Address elements
        	PerfTests.Classes.AzureEntityFramework.Address[] _KnownAddresses_k__BackingFieldValue = s_Accessor4_KnownAddresses_k__BackingField.Get(serializedObject);
        	new SZRefArraySerializer< PerfTests.Classes.AzureEntityFramework.Address >(SerializerStorage,
        	    elem => { new PerfTests_Classes_AzureEntityFramework_AddressSerializer(SerializerStorage, ObjectCache, ValObjectsCache).Serialize(elem); },
        	    () => new PerfTests_Classes_AzureEntityFramework_AddressSerializer(SerializerStorage, ObjectCache, ValObjectsCache).Deserialize()
        	    ).Serialize(_KnownAddresses_k__BackingFieldValue);
        }
        
        public PerfTests.Classes.AzureEntityFramework.Person Deserialize()
        {
            // Result object
            PerfTests.Classes.AzureEntityFramework.Person result = new PerfTests.Classes.AzureEntityFramework.Person();
        
            // Do we have to load full data (when it's not null or cached object)?
            if ( ! ShouldLoadFullData(ref result))
                return result;
        
        	// <PersonID>k__BackingField - WKT Deserialization
        	Guid _PersonID_k__BackingFieldValue = WKTSerializers.Guid.Deserialize();
        	s_Accessor4_PersonID_k__BackingField.Set(result, _PersonID_k__BackingFieldValue);
        
        	// <FirstName>k__BackingField - WKT Deserialization
        	String _FirstName_k__BackingFieldValue = WKTSerializers.String.Deserialize();
        	s_Accessor4_FirstName_k__BackingField.Set(result, _FirstName_k__BackingFieldValue);
        
        	// <LastName>k__BackingField - WKT Deserialization
        	String _LastName_k__BackingFieldValue = WKTSerializers.String.Deserialize();
        	s_Accessor4_LastName_k__BackingField.Set(result, _LastName_k__BackingFieldValue);
        
        	// <BirthDate>k__BackingField - WKT Deserialization
        	DateTime _BirthDate_k__BackingFieldValue = WKTSerializers.DateTime.Deserialize();
        	s_Accessor4_BirthDate_k__BackingField.Set(result, _BirthDate_k__BackingFieldValue);
        
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
        
        	// <Sallary1>k__BackingField - WKT Deserialization
        	Decimal _Sallary1_k__BackingFieldValue = WKTSerializers.Decimal.Deserialize();
        	s_Accessor4_Sallary1_k__BackingField.Set(result, _Sallary1_k__BackingFieldValue);
        
        	// <Sallary2>k__BackingField - WKT Deserialization
        	Decimal _Sallary2_k__BackingFieldValue = WKTSerializers.Decimal.Deserialize();
        	s_Accessor4_Sallary2_k__BackingField.Set(result, _Sallary2_k__BackingFieldValue);
        
        	// <Sallary3>k__BackingField - WKT Deserialization
        	Decimal _Sallary3_k__BackingFieldValue = WKTSerializers.Decimal.Deserialize();
        	s_Accessor4_Sallary3_k__BackingField.Set(result, _Sallary3_k__BackingFieldValue);
        
        	// <Sallary4>k__BackingField - WKT Deserialization
        	Decimal _Sallary4_k__BackingFieldValue = WKTSerializers.Decimal.Deserialize();
        	s_Accessor4_Sallary4_k__BackingField.Set(result, _Sallary4_k__BackingFieldValue);
        
        	// <Sallary5>k__BackingField - WKT Deserialization
        	Decimal _Sallary5_k__BackingFieldValue = WKTSerializers.Decimal.Deserialize();
        	s_Accessor4_Sallary5_k__BackingField.Set(result, _Sallary5_k__BackingFieldValue);
        
        	// <Result1>k__BackingField - WKT Deserialization
        	Double _Result1_k__BackingFieldValue = WKTSerializers.Double.Deserialize();
        	s_Accessor4_Result1_k__BackingField.Set(result, _Result1_k__BackingFieldValue);
        
        	// <Result2>k__BackingField - WKT Deserialization
        	Double _Result2_k__BackingFieldValue = WKTSerializers.Double.Deserialize();
        	s_Accessor4_Result2_k__BackingField.Set(result, _Result2_k__BackingFieldValue);
        
        	// <Result3>k__BackingField - WKT Deserialization
        	Double _Result3_k__BackingFieldValue = WKTSerializers.Double.Deserialize();
        	s_Accessor4_Result3_k__BackingField.Set(result, _Result3_k__BackingFieldValue);
        
        	// <Result4>k__BackingField - WKT Deserialization
        	Double _Result4_k__BackingFieldValue = WKTSerializers.Double.Deserialize();
        	s_Accessor4_Result4_k__BackingField.Set(result, _Result4_k__BackingFieldValue);
        
        	// <Result5>k__BackingField - WKT Deserialization
        	Double _Result5_k__BackingFieldValue = WKTSerializers.Double.Deserialize();
        	s_Accessor4_Result5_k__BackingField.Set(result, _Result5_k__BackingFieldValue);
        
        	// <Skill1>k__BackingField - WKT Deserialization
        	String _Skill1_k__BackingFieldValue = WKTSerializers.String.Deserialize();
        	s_Accessor4_Skill1_k__BackingField.Set(result, _Skill1_k__BackingFieldValue);
        
        	// <Skill2>k__BackingField - WKT Deserialization
        	String _Skill2_k__BackingFieldValue = WKTSerializers.String.Deserialize();
        	s_Accessor4_Skill2_k__BackingField.Set(result, _Skill2_k__BackingFieldValue);
        
        	// <Skill3>k__BackingField - WKT Deserialization
        	String _Skill3_k__BackingFieldValue = WKTSerializers.String.Deserialize();
        	s_Accessor4_Skill3_k__BackingField.Set(result, _Skill3_k__BackingFieldValue);
        
        	// <Skill4>k__BackingField - WKT Deserialization
        	String _Skill4_k__BackingFieldValue = WKTSerializers.String.Deserialize();
        	s_Accessor4_Skill4_k__BackingField.Set(result, _Skill4_k__BackingFieldValue);
        
        	// <Skill5>k__BackingField - WKT Deserialization
        	String _Skill5_k__BackingFieldValue = WKTSerializers.String.Deserialize();
        	s_Accessor4_Skill5_k__BackingField.Set(result, _Skill5_k__BackingFieldValue);
        
        	// <KnownAddresses>k__BackingField - the array of normal elements - element type is PerfTests.Classes.AzureEntityFramework.Address
            PerfTests.Classes.AzureEntityFramework.Address[] _KnownAddresses_k__BackingFieldValue = new SZRefArraySerializer< PerfTests.Classes.AzureEntityFramework.Address >(SerializerStorage,
                elem => { new PerfTests_Classes_AzureEntityFramework_AddressSerializer(SerializerStorage, ObjectCache, ValObjectsCache).Serialize(elem); },
        	    () => new PerfTests_Classes_AzureEntityFramework_AddressSerializer(SerializerStorage, ObjectCache, ValObjectsCache).Deserialize()
                ).Deserialize();
            s_Accessor4_KnownAddresses_k__BackingField.Set(result, _KnownAddresses_k__BackingFieldValue);
        
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


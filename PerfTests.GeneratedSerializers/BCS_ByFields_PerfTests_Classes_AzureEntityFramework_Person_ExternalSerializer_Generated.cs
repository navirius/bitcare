// -----------------------------------------------------------------------------------------------------------------------------
// This is file generated for external serialization of type PerfTests.Classes.AzureEntityFramework.Person - Don't modify it manually !!!
// -----------------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using eSoft.Serializer.Compression;

namespace BitCareSerializer.ByFields.v1 
{
    public partial class PersonSerializer : eSoft.Serializer.Serializer
    {
        
        // Serialization of PerfTests.Classes.AzureEntityFramework.Person (strongly typed - no runtime reflection)
        public static byte[] Serialize_PerfTests_Classes_AzureEntityFramework_Person(PerfTests.Classes.AzureEntityFramework.Person objectToSerialize, CompressionType compressionType = CompressionType.Internal, int compressionLevel = 1)
        {
        #if SILVERLIGHT != true
            ReflectionPermission memberAccessPerm = new ReflectionPermission(PermissionState.Unrestricted);
            memberAccessPerm.Demand();
        #endif
        
            PersonSerializer mainSerializer = new PersonSerializer();
        	mainSerializer.ActiveCompressionType = compressionType;
        	mainSerializer.ActiveCompressionLevel = compressionLevel;
        	
            PerfTests_Classes_AzureEntityFramework_PersonSerializer objectSerializer = new PerfTests_Classes_AzureEntityFramework_PersonSerializer(mainSerializer, mainSerializer.RefObjectsCache, mainSerializer.ValObjectsCache);
            objectSerializer.Serialize(objectToSerialize);
        
            // Final result
            return mainSerializer.ToByteArray();
        }
        
        // Deserialization of PerfTests.Classes.AzureEntityFramework.Person  (strongly typed - no runtime reflection)
        public static PerfTests.Classes.AzureEntityFramework.Person  Deserialize_PerfTests_Classes_AzureEntityFramework_Person(byte[] serializedObject)
        {
        #if SILVERLIGHT != true
            ReflectionPermission memberAccessPerm = new ReflectionPermission(PermissionState.Unrestricted);
            memberAccessPerm.Demand();
        #endif
        
            PersonSerializer mainSerializer = new PersonSerializer();
            mainSerializer.InitStoresFromSerializedData(serializedObject);
        
            PerfTests_Classes_AzureEntityFramework_PersonSerializer objectDeserializer = new PerfTests_Classes_AzureEntityFramework_PersonSerializer(mainSerializer, mainSerializer.RefObjectsCache, mainSerializer.ValObjectsCache);
            return objectDeserializer.Deserialize();
        }
	}
}

// -----------------------------------------------------------------------------------------------------------------------------
// End of file definition for external serialization of type PerfTests.Classes.AzureEntityFramework.Person
// -----------------------------------------------------------------------------------------------------------------------------


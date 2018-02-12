namespace BitCareSerializer.ByFields.v1
{
    public partial class  PersonSerializer : eSoft.Serializer.Serializer
    {
        // We call initialization for complex objects that have field accessors
        public static void InitSerializers()
        {
        	PerfTests_Classes_AzureEntityFramework_PersonSerializer.Init();
        	PerfTests_Classes_AzureEntityFramework_AddressSerializer.Init();

			// End of initialization code
        }
    }
}

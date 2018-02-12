using PerfTests.Classes.AzureEntityFramework;
using eSoft.Serializer.Compression;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfTests.TestApp
{
    public class TestDefinition4PrivateFieldAccessors
    {
        private static Person m_TestObject = null;
        private static int m_NetSerializationSize = 0;
        private static int m_eSoftSerializationSize = 0;

        public static void SerTests()
        {
            int counter = 5000;
            BitCareSerializer.ByFields.v1.PersonSerializer.InitSerializers();

            m_TestObject = new Person
            {
                PersonID = Guid.NewGuid(),
                FirstName = "FirstName",
                LastName = "LastName",
                BirthDate = DateTime.Now,
                KnownAddresses = new Address[]
                {
                    new Address{StreetName="Street",City="City",Building="100",Country="Country",PostCode="123456",ExternalId1=Guid.NewGuid()},
                    new Address{StreetName="Street",City="City",Building="100",Country="Country",PostCode="123456",ExternalId1=Guid.NewGuid()},
                    new Address{StreetName="Street",City="City",Building="100",Country="Country",PostCode="123456",ExternalId1=Guid.NewGuid()},
                    new Address{StreetName="Street",City="City",Building="100",Country="Country",PostCode="123456",ExternalId1=Guid.NewGuid()},
                    new Address{StreetName="Street",City="City",Building="100",Country="Country",PostCode="123456",ExternalId1=Guid.NewGuid()}
                }
            };

            // Warmup
            Ser_Net();
            Ser_eSoft();

            byte[] serResult = BitCareSerializer.ByFields.v1.PersonSerializer.Serialize_PerfTests_Classes_AzureEntityFramework_Person(m_TestObject, CompressionType.Internal);
            m_eSoftSerializationSize = serResult.Length;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                formatter.Serialize(ms, m_TestObject);
                byte[] serStream = ms.ToArray();
                m_NetSerializationSize = serStream.Length;
            }

            Stopwatch sw = new Stopwatch();

            // .Net
            sw.Start();
            for (int pos = 0; pos < counter; pos++)
                Ser_Net();
            sw.Stop();
            Console.WriteLine("Result for .Net binary serialization (in ms) is " + sw.ElapsedMilliseconds + ", size is " + m_NetSerializationSize.ToString());

            // eSoft
            sw.Reset();
            sw.Start();
            for (int pos = 0; pos < counter; pos++)
                Ser_eSoft();
            sw.Stop();
            Console.WriteLine("Result for eSoft contract based serializations (in ms) is " + sw.ElapsedMilliseconds + ", size is " + m_eSoftSerializationSize.ToString());
        }

        private static void Ser_eSoft()
        {
            byte[] serResult = BitCareSerializer.ByFields.v1.PersonSerializer.Serialize_PerfTests_Classes_AzureEntityFramework_Person(m_TestObject, CompressionType.Internal);
            BitCareSerializer.ByFields.v1.PersonSerializer.Deserialize_PerfTests_Classes_AzureEntityFramework_Person(serResult);
        }

        private static void Ser_Net()
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                formatter.Serialize(ms, m_TestObject);
                byte[] serStream = ms.ToArray();
                ms.Seek(0, SeekOrigin.Begin);
                formatter.Deserialize(ms);
            }
        }
    }
}

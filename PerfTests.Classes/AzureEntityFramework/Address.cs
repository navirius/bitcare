using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfTests.Classes.AzureEntityFramework
{
    [Serializable]
    public class Address
    {
        public string StreetName { get; set; }
        public string Building { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public Guid ExternalId1 { get; set; }
        public Guid ExternalId2 { get; set; }
        public Guid ExternalId3 { get; set; }
        public Guid ExternalId4 { get; set; }
        public Guid ExternalId5 { get; set; }
        
    }
}

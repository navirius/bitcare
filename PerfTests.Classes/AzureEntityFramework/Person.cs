using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfTests.Classes.AzureEntityFramework
{
    [Serializable]
    public class Person
    {
        public Person()
        {
            //m_PrivateStateField1 = 1;
            //m_PrivateStateField2 = 2;
        }

        public Guid PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        //public DateTime? OptionalBirthDate { get; set; }
        public Address[] KnownAddresses { get; set; }

        // private members

        //private int m_PrivateStateField1 { get; set; }
        //private int m_PrivateStateField2 { get; set; }

        // Some test properties 

        public Guid ExternalId1 { get; set; }
        public Guid ExternalId2 { get; set; }
        public Guid ExternalId3 { get; set; }
        public Guid ExternalId4 { get; set; }
        public Guid ExternalId5 { get; set; }

        public decimal Sallary1 { get; set; }
        public decimal Sallary2 { get; set; }
        public decimal Sallary3 { get; set; }
        public decimal Sallary4 { get; set; }
        public decimal Sallary5 { get; set; }

        public double Result1 { get; set; }
        public double Result2 { get; set; }
        public double Result3 { get; set; }
        public double Result4 { get; set; }
        public double Result5 { get; set; }

        public string Skill1 { get; set; }
        public string Skill2 { get; set; }
        public string Skill3 { get; set; }
        public string Skill4 { get; set; }
        public string Skill5 { get; set; }
    }
}

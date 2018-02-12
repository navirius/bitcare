using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfTests.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Serialization by private fields\n");
            TestDefinition4PrivateFieldAccessors.SerTests();

            Console.WriteLine("\n\nSerialization by public properties\n");
            TestDefinition4PublicMembersAccessors.SerTests();

            Console.WriteLine();
        }
    }
}

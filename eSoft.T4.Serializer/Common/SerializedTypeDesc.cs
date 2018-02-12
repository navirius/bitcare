// -----------------------------------------------------------------------
// <copyright file="SerializedClassDesc.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.T4.Serializer
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SerializedTypeDesc
    {
        public RuntimeTypeHandle TypeHandle { get; set; }
        public Type Type { get { return Type.GetTypeFromHandle(TypeHandle); } }

        public string FullName { get { return this.Type.FullName; } }
        public string SafeFullName { get { return this.FullName.Replace("[]", "_Array").Replace(".", "_"); } }

        public string Name { get { return this.Type.Name; } }
        public string SafeName { get { return this.Name.Replace("[]", "_Array").Replace(".", "_"); } }

        // When the field is based on type that has derived types we assume they can be stored also there
        // Key is field name, value is info about derived type
        public Dictionary<string, DerivedTypesDesc> DerivedTypes = new Dictionary<string, DerivedTypesDesc>();
    }
}

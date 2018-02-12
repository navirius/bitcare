// -----------------------------------------------------------------------
// <copyright file="UnknownTypeSerializer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.SpecialSerializers
{
    /// <summary>
    /// This is serializer, that can be called when serializer doesn't know (de)serialized type.
    /// Such situation can occur when new derived type has been stored in some field or field is an object 
    /// and this new type has not been listed excplicitly on types for serialization list
    /// </summary>
    public class UnknownTypeSerializer
    {
    }
}

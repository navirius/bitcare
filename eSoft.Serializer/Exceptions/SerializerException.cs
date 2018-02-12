// -----------------------------------------------------------------------
// <copyright file="SerializerException.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace eSoft.Serializer.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SerializerException:Exception
    {
        public SerializerException(){}
        public SerializerException(string message) : base(message) { }
    }
}

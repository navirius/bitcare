using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.Exceptions
{
    public class UnknownPolymorphicTypeException : SerializerException
    {
        public UnknownPolymorphicTypeException() { }
        public UnknownPolymorphicTypeException(string message) : base(message) { }
    }
}

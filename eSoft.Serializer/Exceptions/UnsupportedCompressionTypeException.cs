// -----------------------------------------------------------------------
// <copyright file="UnsupportedCompressionTypeException.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.Exceptions
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UnsupportedCompressionTypeException : SerializerException
    {
        public UnsupportedCompressionTypeException() { }
        public UnsupportedCompressionTypeException(string message) : base(message) { }
    }
}

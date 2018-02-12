// -----------------------------------------------------------------------
// <copyright file="IConfigCase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eSoft.Serializer.Infrastructure;

namespace eSoft.Serializer.Infrastructure.StorageFormat
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IStorageFormat
    {
        byte FormatId { get; set; }       // Numeric config case Id
        byte FormatIdSize { get; }        // Size of config case Id in bits

        ConfigBits FormatConfig { get; }  // Case config bits
    }
}

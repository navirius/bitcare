// -----------------------------------------------------------------------
// <copyright file="IStorageFormatTyped.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.Infrastructure.StorageFormat
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IStrongFormatType<FormatIdEnumType>
    {
        FormatIdEnumType FormatType { get; }  // Casted to local enumeration
    }
}

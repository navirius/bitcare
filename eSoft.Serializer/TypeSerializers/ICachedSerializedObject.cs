// -----------------------------------------------------------------------
// <copyright file="ICachedSerializedObject.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.TypeSerializers
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICachedSerializedObject : ISerializedObject
    {
        bool IsRegisteredAlready { get; set; }
        int ObjectId { get; set; }
    }
}

﻿// -----------------------------------------------------------------------
// <copyright file="DefaultValue.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.TypeSerializers.WellKnown.Single_StorageFormats
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DefaultValue : SingleStorageBase
    {
        public const int UsedConfigBitsForCase = 0;    

        // Constructor
        public DefaultValue() : base(SingleStorageFormats.DefaultValue, UsedConfigBitsForCase) { }
    }
}

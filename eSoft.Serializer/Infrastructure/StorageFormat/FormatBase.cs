// -----------------------------------------------------------------------
// <copyright file="ConfigCase.cs" company="">
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
    public class FormatBase
    {
        public byte         FormatId          { get; set; }
        public byte         FormatIdSize      { get; set; } // Size is in bits
        
        public ConfigBits   FormatConfig      { get; set; }

        public FormatBase(byte formatId, byte formatIdSize, byte usedConfigBits)
        {
            FormatId = formatId;
            FormatIdSize = formatIdSize;
            FormatConfig = new ConfigBits();
            FormatConfig.SizeInBits = usedConfigBits;
        }
    }
}

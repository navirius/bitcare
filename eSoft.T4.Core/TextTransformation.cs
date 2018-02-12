using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using eSoft.T4.Core.FileBlocksManager;

using BaseHelper = Microsoft.VisualStudio.TextTemplating.ToStringHelper;

namespace eSoft.T4.Core
{
    public abstract class TextTransformation: Microsoft.VisualStudio.TextTemplating.TextTransformation
    {
        // Multi files template manager
        public FilePartsManager Manager { get; set; }

        // Initialization of transformation
        public void InitTransformation(ITextTemplatingEngineHost host)
        {
            this.Manager = FilePartsManager.Create(host, GenerationEnvironment);
        }

        private string m_Indent = "    "; // By default, indent uses spaces.
        public string Indent { get { return this.m_Indent; } set { this.m_Indent = value; } }

        /// <summary>
        /// Indent by a standardized amount
        /// </summary>
        public virtual void PushIndent()
        {
            this.PushIndent(this.Indent);
        }
        /// <summary>
        /// Write a new line
        /// </summary>
        public virtual void NewLine()
        {
            this.WriteLine("");
        }

        public virtual void Underscore()
        {
            this.WriteLine
            (
            "// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------"
            );
        }

        private Lazy<ToStringHelperShim> toStringHelper = new Lazy<ToStringHelperShim>();
        public ToStringHelperShim ToStringHelper
        {
            get
            {
                return this.toStringHelper.Value;
            }
        }

        /// <summary>
        /// Class to provide an instance-based shim for the ToStringHelper duck-type.
        /// </summary>
        public class ToStringHelperShim
        {
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression
            /// block
            /// to a string
            /// </summary>
            /// <param name="objectToConvert">The object to convert to a string</param>
            /// <returns>The object converted to a string using the template's culture</returns>
            public string ToStringWithCulture(object objectToConvert)
            {
                return BaseHelper.ToStringWithCulture(objectToConvert);
            }

            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public static System.IFormatProvider FormatProvider
            {
                get { return BaseHelper.FormatProvider; }
                set { BaseHelper.FormatProvider = value; }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using eSoft.T4.Core;

namespace eSoft.T4.Serializer.ByPrivateFields
{
    public partial class Serializer : TextTransformation
    {
        private void External_Serialization(SerializedTypeDesc item)
        {
            // We don't generate serializer for array types and well know types
            if (item.Type.IsArray || IsWellKnownType(item))
                return;

            // Separate file
            StartNewFile(item.SafeFullName + "_ExternalSerializer");
            External_Header(item);

            this.PushIndent();
            this.PushIndent();
            EX_Normal(item);
            this.PopIndent();
            this.PopIndent();

            External_Footer(item);
            Manager.RestorePreviousBlock();
        }
    }
}

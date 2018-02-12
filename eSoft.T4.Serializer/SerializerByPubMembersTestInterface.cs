using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.T4.Serializer.ByPublicMembers
{
    public class SerializerByPubMembersTestInterface : Serializer
    {
        public string DoTest()
        {
            TransformText();
            string result = this.GenerationEnvironment.ToString();
            return result;
        }
    }
}

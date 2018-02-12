using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.T4.Serializer.ByPrivateFields
{
    public class SerializerByFieldsTestInterface : Serializer
    {
        public string DoTest()
        {
            TransformText();
            string result = this.GenerationEnvironment.ToString();
            return result;
        }
    }
}

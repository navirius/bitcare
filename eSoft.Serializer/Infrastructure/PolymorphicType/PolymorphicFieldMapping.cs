using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.Serializer.Infrastructure.PolimorphicType
{
    public class PolymorphicFieldMapping
    {
        private Dictionary<string, PolymorphicTypeMapping> m_FieldMapping;

        public PolymorphicFieldMapping()
        {
            m_FieldMapping = new Dictionary<string,PolymorphicTypeMapping>();
        }

        public PolymorphicTypeMapping GetFieldMapping(string fieldName)
        {
            if (m_FieldMapping.ContainsKey(fieldName))
                return m_FieldMapping[fieldName];

            PolymorphicTypeMapping mapping = new PolymorphicTypeMapping();
            m_FieldMapping.Add(fieldName, mapping);

            return mapping;
        }
    }
}
